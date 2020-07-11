using System;
using System.Reflection;
using AutoMapper;
using Core.Infrastructure.Presentation.GraphQL.Constants;
using Core.Infrastructure.Presentation.GraphQL.Extensions;
using Core.Infrastructure.Presentation.GraphQL.Schemas;
using CorrelationId;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.GraphQL
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">
        ///     The application configuration, where key value pair settings are stored. See
        ///     http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// </param>
        /// <param name="hostingEnvironment">
        ///     The environment the application is running under. This can be Development,
        ///     Staging or Production by default. See http://docs.asp.net/en/latest/fundamentals/environments.html
        /// </param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///     Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        ///     called by the ASP.NET runtime. See
        ///     http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorrelationIdFluent();
            services.AddCustomCaching();
            services.AddCustomOptions(configuration);
            services.AddCustomRouting();
            services.AddCustomResponseCompression();
            services.AddCustomStrictTransportSecurity();
            services.AddCustomHealthChecks();
            services.AddHttpContextAccessor();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddAuthorization()
                //.AddJsonFormatters()
                //.AddCustomJsonOptions(this.hostingEnvironment)
                .AddCustomCors()
                .AddCustomMvcOptions(this.hostingEnvironment);

            //services.ConfigureCors();
            services.AddCustomGraphQL(hostingEnvironment);
            services.ConfigureMySqlContext(configuration);
            services.ConfigureUnitOfWork();
            services.ConfigureDomainService();
            services.ConfigureApplicationService();
            services.ConfigureProjectSchemas();
            services.ConfigureMediatr();
            services.BuildServiceProvider();
            services.ConfigureAuthentication(configuration);
            services.AddAutoMapper(typeof(Startup));
            //MappingExtensions();
        }

        public void Configure(IApplicationBuilder application)
        {
            application
                                // Pass a GUID in a X-Correlation-ID HTTP header to set the HttpContext.TraceIdentifier.
                                // UpdateTraceIdentifier must be false due to a bug. See https://github.com/aspnet/AspNetCore/issues/5144
                                //.UseCorrelationId(new CorrelationIdOptions {UpdateTraceIdentifier = false})
                                //.UseCorrelationId(new CorrelationIdOptions() { UpdateTraceIdentifier = false })
                                .UseForwardedHeaders()
                                .UseResponseCompression()
                .UseCors(CorsPolicyName.AllowAny)
                .UseIf(
                    !hostingEnvironment.IsDevelopment(),
                    x => x.UseHsts())
                .UseIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x.UseDeveloperErrorPages())
                .UseHealthChecks("/status")
                .UseHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false })
                //.UseStaticFilesWithCacheControl()
                .UseWebSockets()
                // Use the GraphQL subscriptions in the specified schema and make them available at /graphql.
                .UseGraphQLWebSockets<MainSchema>()
                // Use the specified GraphQL schema and make them available at /graphql.
                .UseGraphQL<MainSchema>()
                .UseIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x
                        // Add the GraphQL Playground UI to try out the GraphQL API at /.
                        .UseGraphQLPlayground(new GraphQLPlaygroundOptions { Path = "/" })
                        // Add the GraphQL Voyager UI to let you navigate your GraphQL API as a spider graph at /voyager.
                        .UseGraphQLVoyager(new GraphQLVoyagerOptions { Path = "/voyager" }));
        }
    }
}
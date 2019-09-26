using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Contract.Service;
using Core.Infrastructure.Presentation.GraphQL.Constants;
using Core.Infrastructure.Presentation.GraphQL.Executer;
using Core.Infrastructure.Presentation.GraphQL.Options;
using CorrelationId;
using GraphQL;
using GraphQL.Authorization;
using GraphQL.Server;
using GraphQL.Server.Internal;
using GraphQL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using IAuthorizationEvaluator = Microsoft.AspNetCore.Authorization.IAuthorizationEvaluator;

namespace Core.Infrastructure.Presentation.GraphQL.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
    /// </summary>
    public static class GraphQLExtensions
    {

        public static void AddCustomGraphQL(this IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            services
                            // Add a way for GraphQL.NET to resolve types.
                            .AddScoped<IDependencyResolver, GraphQLDependencyResolver>()
                            .AddGraphQL(
                                options =>
                                {
                                    var configuration = services
                                        .BuildServiceProvider()
                                        .GetRequiredService<IOptions<GraphQLOptions>>()
                                        .Value;
                                    // Set some limits for security, read from configuration.
                                    options.ComplexityConfiguration = configuration.ComplexityConfiguration;
                                    // Enable GraphQL metrics to be output in the response, read from configuration.
                                    options.EnableMetrics = configuration.EnableMetrics;
                                    // Show stack traces in exceptions. Don't turn this on in production.
                                    options.ExposeExceptions = hostingEnvironment.IsDevelopment();
                                })
                            // Adds all graph types in the current assembly with a singleton lifetime.
                            .AddGraphTypes()
                            // Adds ConnectionType<T>, EdgeType<T> and PageInfoType.
                            .AddRelayGraphTypes()
                            // Add a user context from the HttpContext and make it available in field resolvers.
                            //.AddUserContextBuilder<GraphQLUserContextBuilder>()
                            // Add GraphQL data loader to reduce the number of calls to our repository.
                            .AddDataLoader()
                            // Add WebSockets support for subscriptions.
                            .AddWebSockets()
                            .Services
                            .AddTransient(typeof(IGraphQLExecuter<>), typeof(InstrumentingGraphQLExecutor<>));
        }
    }
}

using AutoMapper;
using Core.Infrastructure.Domain.Context.Context;
using Core.Infrastructure.Presentation.API.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAuthentication(Configuration);
            services.AddMvc().AddFluentValidation();
            services.ConfigureLogger(Configuration);
            services.ConfigureCors();
            services.ConfigureMySqlContext(Configuration);
            services.ConfigureUnitOfWork();
            services.ConfigureSwagger();
            services.AddAutoMapper();
            services.ConfigureApplicationService();
            services.ConfigureDomainService();
            services.ConfigureFluentValidation();
            services.ConfigureRedisCache();
            Mapping.ConfigureMapping();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Context context)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            // ===== Use Authentication ======
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/CoreInfrastructure/swagger.json", "CoreInfrastructure"); });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "api/{controller}/{action}");
                //defaults: new { controller = "RefType" });
                //routes.MapRoute(
                //    name: "LogsId",
                //    template: "api/[controller]/ID/{id}",
                //    defaults: new { controller = "BXLogs", action = "GetById" });

                //routes.MapRoute(
                //    name: "LogsAPI",
                //    template: "api/[controller]/API/{apiname}",
                //    defaults: new { controller = "BXLogs", action = "GetByAPI" });
            });
        }
    }
}
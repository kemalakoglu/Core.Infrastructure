using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public static class Swagger
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoreInfrastructure", new Info
                {
                    Title = "Core.Infrastructure API",
                    Version = "Core.Infrastructure",
                    Description = "Core.Infrastructure Web API Documentation",
                    Contact = new Contact
                    {
                        Name = "Swagger Implementation of Kemal Akoğlu",
                        //Url = "http://doco.com.tr",
                        Email = "kemal.akoglu@doco.com.tr"
                    }
                });
            });
        }
    }
}
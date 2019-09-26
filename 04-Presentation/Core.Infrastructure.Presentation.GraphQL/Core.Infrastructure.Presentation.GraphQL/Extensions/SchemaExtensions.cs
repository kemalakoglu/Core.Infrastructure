using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Presentation.GraphQL.Schemas;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.GraphQL.Extensions
{
    public static class SchemaExtensions
    {
        /// <summary>
        /// Add project GraphQL schema and web socket types.
        /// </summary>
        public static void ConfigureProjectSchemas(this IServiceCollection services)=>
            services.AddScoped<MainSchema>();
    }
}

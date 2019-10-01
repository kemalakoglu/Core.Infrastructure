using Core.Infrastructure.Presentation.GraphQL.Graphs.RefType;
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
            services.AddScoped<MainSchema>().AddScoped<RefTypeSchema>().AddScoped<RefTypeGraph>().AddScoped<GetRefTypesResponseGraph>();
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public static class FluentValidator
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            //services.AddTransient<IValidator<Domain.Context.Entities.Galley>, GalleyValidator>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public static class Redis
    {
        public static void ConfigureRedisCache(this IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379";
            });
        }
    }
}

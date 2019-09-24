using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Application.Service;
using Core.Infrastructure.Application.UnitOfWork;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Context.Context;
using Core.Infrastructure.Domain.Contract.Service;
using Core.Infrastructure.Domain.Repository;
using Core.Infrastructure.Presentation.GraphQL.Schemas;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.GraphQL.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            return services.AddDbContext<CoreContext>(o => o.UseSqlServer(connectionString));

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<HostingApplication.Context>()
            //    .AddDefaultTokenProviders();
        }

        public static IServiceCollection ConfigureUnitOfWork(this IServiceCollection services) =>
             services.AddSingleton<IUnitOfWork, UnitOfWork>();


        public static IServiceCollection ConfigureDomainService(this IServiceCollection services) =>
            services.AddSingleton<IRefTypeService, RefTypeService>()
                .AddSingleton<IUserStoreService, UserStoreService>();

        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services) =>
            services.AddSingleton<ICoreApplicationService, CoreApplicationService>();


        public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services) =>
            services.AddSingleton<IRepositoryWrapper, RepositoryWrapper>();

    }
}

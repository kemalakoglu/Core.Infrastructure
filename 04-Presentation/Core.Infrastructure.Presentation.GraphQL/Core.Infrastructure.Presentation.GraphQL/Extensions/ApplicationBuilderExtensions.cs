using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Presentation.GraphQL.Constants;
using Core.Infrastructure.Presentation.GraphQL.Exception;
using Core.Infrastructure.Presentation.GraphQL.Middleware;
using Core.Infrastructure.Presentation.GraphQL.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Presentation.GraphQL.Extensions
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                // When a database error occurs, displays a detailed error page with full diagnostic information. It is
                // unsafe to use this in production. Uncomment this if using a database.
                // .UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                // When an error occurs, displays a detailed error page with full diagnostic information.
                // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                .UseDeveloperExceptionPage();

        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            var cacheProfile = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptions>()
                .Where(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault();
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            context.Context.ApplyCacheProfile(cacheProfile);
                        },
                    });
        }

        /// <summary>
        /// Allows the use of <see cref="HttpException"/> as an alternative method of returning an error result.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseHttpException(this IApplicationBuilder application) => UseHttpException(application, null);

        /// <summary>
        /// Allows the use of <see cref="HttpException"/> as an alternative method of returning an error result.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="configureOptions">The middleware options.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseHttpException(this IApplicationBuilder application, Action<HttpExceptionMiddlewareOptions> configureOptions)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            var options = new HttpExceptionMiddlewareOptions();
            configureOptions?.Invoke(options);
            return application.UseMiddleware<HttpExceptionMiddleware>(options);
        }

        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the request execution pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                application = action(application);
            }

            return application;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the request execution pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the request execution pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the request execution pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition)
            {
                application = ifAction(application);
            }
            else
            {
                application = elseAction(application);
            }

            return application;
        }

        /// <summary>
        /// Executes the specified action using the <see cref="HttpContext"/> to determine if the specified
        /// <paramref name="condition"/> is <c>true</c> which can be used to conditionally add to the request execution
        /// pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            Func<HttpContext, bool> condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var builder = application.New();

            action(builder);

            return application.Use(next =>
            {
                builder.Run(next);

                var branch = builder.Build();

                return context =>
                {
                    if (condition(context))
                    {
                        return branch(context);
                    }

                    return next(context);
                };
            });
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> using the <see cref="HttpContext"/> to determine if the
        /// specified <paramref name="condition"/> is <c>true</c>, otherwise executes the
        /// <paramref name="elseAction"/>. This can be used to conditionally add to the request execution pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the request execution pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the request execution pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            Func<HttpContext, bool> condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            var ifBuilder = application.New();
            var elseBuilder = application.New();

            ifAction(ifBuilder);
            elseAction(elseBuilder);

            return application.Use(next =>
            {
                ifBuilder.Run(next);
                elseBuilder.Run(next);

                var ifBranch = ifBuilder.Build();
                var elseBranch = elseBuilder.Build();

                return context =>
                {
                    if (condition(context))
                    {
                        return ifBranch(context);
                    }
                    else
                    {
                        return elseBranch(context);
                    }
                };
            });
        }

        /// <summary>
        /// Returns a 500 Internal Server Error response when an unhandled exception occurs.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseInternalServerErrorOnException(this IApplicationBuilder application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return application.UseMiddleware<InternalServerErrorOnExceptionMiddleware>();
        }
    }
}

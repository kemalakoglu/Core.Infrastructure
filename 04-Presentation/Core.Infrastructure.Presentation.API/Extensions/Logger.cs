using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public static class Logger
    {
        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
           
           //services.AddScoped<ISerilogLogger, SerilogLogger>();
           Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .Enrich.WithProperty("Application", "app")
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override("System", LogEventLevel.Warning)
               //.WriteTo.File(new JsonFormatter(), "log.json")
               //.ReadFrom.Configuration(configuration)
               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("localhost:9200"))
               {
                   AutoRegisterTemplate = true,
                   AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                   FailureCallback = e => Console.WriteLine("fail message: " + e.MessageTemplate),
                   EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                      EmitEventFailureHandling.WriteToFailureSink |
                                      EmitEventFailureHandling.RaiseCallback,
                   FailureSink = new FileSink("log" + ".json", new JsonFormatter(), null)
               })
               .MinimumLevel.Verbose()
               .CreateLogger();
           Log.Information("WebApi Starting...");
        }
    }
}
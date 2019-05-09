using System;
using Core.Infrastructure.Core.Contract;
using Serilog;
using Serilog.Events;

namespace Core.Infrastructure.Core.Logger
{
    public class SerilogLogger : ISerilogLogger
    {
        public SerilogLogger()
        {
            ConfigureLogger();
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        public void Write(LogEventLevel level, string messageTemplate)
        {
            Log.Write(level, messageTemplate);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            Log.Write(level, messageTemplate, propertyValue);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        public void Write<T0, T1>(
            LogEventLevel level,
            string messageTemplate,
            T0 propertyValue0,
            T1 propertyValue1)
        {
            Log.Write(level, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        public void Write<T0, T1, T2>(
            LogEventLevel level,
            string messageTemplate,
            T0 propertyValue0,
            T1 propertyValue1,
            T2 propertyValue2)
        {
            Log.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Log.Write(level, messageTemplate, propertyValues);
        }

        /// <summary>
        ///     Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        public void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            Log.Write(level, exception, messageTemplate);
        }


        /// <summary>
        ///     Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        public void Write<T>(
            LogEventLevel level,
            Exception exception,
            string messageTemplate,
            T propertyValue)
        {
            Log.Write(level, exception, messageTemplate, propertyValue);
        }


        /// <summary>
        ///     Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        public void Write<T0, T1>(
            LogEventLevel level,
            Exception exception,
            string messageTemplate,
            T0 propertyValue0,
            T1 propertyValue1)
        {
            Log.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        ///     Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        public void Write<T0, T1, T2>(
            LogEventLevel level,
            Exception exception,
            string messageTemplate,
            T0 propertyValue0,
            T1 propertyValue1,
            T2 propertyValue2)
        {
            Log.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }


        /// <summary>
        ///     Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        public void Write(
            LogEventLevel level,
            Exception exception,
            string messageTemplate,
            params object[] propertyValues)
        {
            Log.Write(level, exception, messageTemplate, propertyValues);
        }

        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Core.Infrastructure.Presentation.API")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.File("log.txt",
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .MinimumLevel.Verbose()
                .CreateLogger();
            Log.Information("WebApi Starting...");
        }
    }
}
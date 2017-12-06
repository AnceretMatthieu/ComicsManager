using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace ComicsManager.API.Filters
{
    /// <summary>
    /// Handler global pour les exceptions
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalExceptionsFilter(
            ILoggerFactory logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger.CreateLogger<GlobalExceptionsFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "");
        }
    }
}

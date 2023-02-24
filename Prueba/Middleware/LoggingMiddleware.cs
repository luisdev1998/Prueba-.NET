using System;
namespace Prueba.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IWebHostEnvironment env)
        {
            this._next = next;
            this._logger = logger;
            this._env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {

                var startTime = DateTime.UtcNow;
                await _next(context);
                var endTime = DateTime.UtcNow;
                var duration = endTime - startTime;
                var controllerName = context.GetRouteData().Values["controller"].ToString();
                var response = $"[{startTime}] {controllerName} {context.Request.Method} {context.Response.StatusCode} {duration.TotalMilliseconds}ms";
                _logger.LogInformation(response);
                WriteLogToFile(response);
            }
        }

        private void WriteLogToFile(string logMessage)
        {
            var ruta = $"{_env.ContentRootPath}/wwwroot/log.txt";

            using var streamWriter = new StreamWriter(ruta, true);
            streamWriter.WriteLine(logMessage);
        }
    }
}


using System.Diagnostics;

namespace TaskManagementAPI
{
    public class RequestTimingMiddleware
    {
        RequestDelegate _next { get; }
        ILogger<RequestTimingMiddleware> _logger { get;}

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            //Console.WriteLine("Starting stopwatch");
            await _next(context);
            stopwatch.Stop();
            //Console.WriteLine($"Stopwatch stopped: {stopwatch}");

            var requestPath = context.Request.Path;
            _logger.LogInformation("{requestPath} took {stopwatch}", context.Request.Path, stopwatch.ElapsedMilliseconds);

        }
    }
}

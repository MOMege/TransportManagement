using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TransportManagement.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse>
        {

        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger) { 

        _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("➡ Handling {RequestName} with Data: {@Request}",
                                  requestName, request);

            var timer = System.Diagnostics.Stopwatch.StartNew();
            var response = await next();
            timer.Stop();

            _logger.LogInformation("⬅ Handled {RequestName} in {Elapsed}ms | Response: {@Response}",
                                  requestName, timer.ElapsedMilliseconds, response);
            return response;

        }

    }
}

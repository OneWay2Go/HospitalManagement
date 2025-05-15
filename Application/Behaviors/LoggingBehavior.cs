using MediatR;
using Serilog;

namespace HospitalManagement.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing: {RequestName}, time: {Time}", typeof(TRequest).Name, DateTime.UtcNow);

            var response = await next(cancellationToken);

            _logger.LogInformation("Executed: {RequestName}, time: {Time}", typeof(TRequest).Name, DateTime.UtcNow);

            return response;
        }
    }
}

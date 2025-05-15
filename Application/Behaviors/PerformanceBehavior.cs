using MediatR;
using System.Diagnostics;

namespace HospitalManagement.Application.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse>(
        ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing: {RequestName}...", typeof(TRequest).Name);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = await next(cancellationToken);

            stopwatch.Stop();
            var timeSpent = stopwatch.ElapsedMilliseconds;

            logger.LogInformation("Executed: {RequestName}, Time Spent: {TimeSpent}ms", typeof(TRequest).Name, timeSpent);

            return response;
        }
    }
}

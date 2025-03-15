using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace HospitalManagement.Filters
{
    public class LogActionFilter : Attribute, IResourceFilter
    {
        private Stopwatch _stopwatch;

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _stopwatch.Stop();

            Console.WriteLine($"Executed: {context.ActionDescriptor.DisplayName} in {_stopwatch.ElapsedMilliseconds}ms");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            Console.WriteLine($"Executing: {context.ActionDescriptor.DisplayName}");
        }
    }
}

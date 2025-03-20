
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace HospitalManagement.Middlewares
{
    public class ConfigurationValidationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(OptionsValidationException e)
            {
                Console.WriteLine($"Failed in validating the settings: {e.Failures}");
            }
        }
    }
}

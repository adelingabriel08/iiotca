using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Signal.Server.Database;

namespace Signal.Server.Attributes;

public class RequireMacAndApiKeyAttribute: Attribute, IAsyncActionFilter
{
    private const string APIKEYNAME = "ApiKey";
    private const string MACNAME = "MAC";
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(MACNAME, out var extractedMac))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Device MAC was not provided"
            };
            return;
        }
        if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api Key was not provided"
            };
            return;
        }

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
        var sensor =
            await dbContext.AuthorizedSensors.FirstOrDefaultAsync(s =>
                s.ApiKey == extractedApiKey && s.MAC == extractedMac);
        
        if (sensor is null)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api Key or MAC is invalid"
            };
            return;
        }

        await next();
    }
}
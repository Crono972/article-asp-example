using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Api.Serialization;

public class ForceSerializationAttribute : ActionFilterAttribute
{
    private readonly JsonSerializerSettings _jsonSettings;

    public ForceSerializationAttribute(string jsonStrategy)
    {
        _jsonSettings = NewtonsoftNamingStrategyProvider.GetJsonSerializerSettings(jsonStrategy);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            objectResult.Formatters.Add(new NewtonsoftJsonOutputFormatter(_jsonSettings,
                context.HttpContext.RequestServices.GetRequiredService<ArrayPool<char>>()
                , context.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>().Value, null));

        }
    }
}
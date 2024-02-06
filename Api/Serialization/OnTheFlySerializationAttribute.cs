using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace Api.Serialization;

public class OnTheFlySerializationAttribute : ActionFilterAttribute
{
    private const string SerializationHeaderKeys = "x-json-case-strategy";
    private const string SerializationParameter = "jsonCaseStrategy";

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            if (context.HttpContext.Request.Headers.TryGetValue(SerializationHeaderKeys, out var jsonStrategy)
            || context.HttpContext.Request.Query.TryGetValue(SerializationParameter, out jsonStrategy))
            {
                var strategy =
                    NewtonsoftNamingStrategyProvider.GetJsonSerializerSettings(jsonStrategy.ToString().ToLower());

                objectResult.Formatters.Add(new NewtonsoftJsonOutputFormatter(strategy,
                    context.HttpContext.RequestServices.GetRequiredService<ArrayPool<char>>()
                    , context.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>().Value, null));
            }
        }
    }
}
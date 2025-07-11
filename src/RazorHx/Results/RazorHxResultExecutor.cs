using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RazorHx.Components;
using RazorHx.DependencyInjection;
using RazorHx.Htmx;
using RazorHx.Htmx.HttpContextFeatures;

namespace RazorHx.Results;

internal static class RazorHxResultExecutor
{
    private const string DefaultContentType = "text/html; charset=utf-8";

    public static async Task ExecuteAsync(HttpContext httpContext, RazorHxResult result)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var response = httpContext.Response;
        response.ContentType = result.ContentType ?? DefaultContentType;
        response.StatusCode = result.StatusCode ?? response.StatusCode;

        PrepareResponseHeaders(httpContext, result);

        var htmlRenderer = httpContext.RequestServices.GetRequiredService<HtmlRenderer>();
        var razorHxComponentsServiceOptions = httpContext.RequestServices.GetRequiredService<RazorHxServiceOptions>();
        var htmxRequestFeature = httpContext.Features.Get<IHtmxRequestFeature>();

        if (htmxRequestFeature == null)
            throw new InvalidOperationException("HtmxRequestFeature is null");

        Type layout;

        if (htmxRequestFeature.CurrentRequest is { Request: true, Boosted: false })
        {
            layout = typeof(EmptyLayout);
        }
        else
        {
            var useComponentLayout = result.ComponentType.GetCustomAttribute<LayoutAttribute>() != null;
            layout = useComponentLayout
                ? result.ComponentType.GetCustomAttribute<LayoutAttribute>()!.LayoutType
                : razorHxComponentsServiceOptions.RootComponent;
        }

        var parameters = new Dictionary<string, object?>
        {
            { "Layout", layout },
            { "ComponentType", result.ComponentType },
            { "Parameters", (Dictionary<string, object?>)result.Parameters.ToDictionary() }
        };

        if (result.OobComponentType != null)
        {
            parameters.Add("OobComponentType", result.OobComponentType);
            parameters.Add("OobParameters", (Dictionary<string, object?>)result.OobParameters.ToDictionary());
        }

        var htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output =
                await htmlRenderer.RenderComponentAsync(typeof(HxLayout), ParameterView.FromDictionary(parameters));
            return output.ToHtmlString();
        });

        await httpContext.Response.WriteAsync(htmlContent);
    }

    private static void PrepareResponseHeaders(HttpContext httpContext, RazorHxResult result)
    {
        foreach (var trigger in result.Triggers)
        {
            switch (trigger.Timing)
            {
                case TriggerTiming.Default:
                    SetHeader(HxResponseHeaderKeys.Trigger, trigger.Name);
                    break;
                case TriggerTiming.AfterSettle:
                    SetHeader(HxResponseHeaderKeys.TriggerAfterSettle, trigger.Name);
                    break;
                case TriggerTiming.AfterSwap:
                    SetHeader(HxResponseHeaderKeys.TriggerAfterSwap, trigger.Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return;

        void SetHeader(string header, string trigger)
        {
            if (httpContext.Response.Headers.Remove(header, out var _))
            {
                httpContext.Response.Headers.Append(header, trigger);
            }
            else
            {
                httpContext.Response.Headers.Append(header, trigger);
            }
        }
    }
}
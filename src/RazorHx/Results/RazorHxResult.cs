using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RazorHx.Components;
using RazorHx.DependencyInjection;
using RazorHx.Extensions;
using RazorHx.Htmx;
using RazorHx.Htmx.HttpContextFeatures;
using RazorHx.Results.Interfaces;

namespace RazorHx.Results;

public class RazorHxResult : IRazorHxResult
{
    private const string DefaultContentType = "text/html; charset=utf-8";

    public string? ContentType { get; set; }
    public int? StatusCode { get; set; }
    public Type ComponentType { get; }
    public ParameterView Parameters { get; }
    public List<(Type, ParameterView)> OutOfBandComponents { get; } = [];
    public List<HtmxTrigger> Triggers { get; } = [];

    protected RazorHxResult(Type componentType)
        : this(componentType, ParameterView.Empty)
    {
    }

    protected RazorHxResult(Type componentType, object parameters)
        : this(componentType, CoerceParametersObjectToDictionary(parameters))
    {
    }

    private RazorHxResult(Type componentType, ParameterView parameters)
    {
        ArgumentNullException.ThrowIfNull(componentType);

        ComponentType = componentType;
        Parameters = parameters;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var response = httpContext.Response;
        response.ContentType = ContentType ?? DefaultContentType;
        response.StatusCode = StatusCode ?? response.StatusCode;

        PrepareResponseHeaders(httpContext);

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
            var useComponentLayout = ComponentType.GetCustomAttribute<LayoutAttribute>() != null;
            layout = useComponentLayout
                ? ComponentType.GetCustomAttribute<LayoutAttribute>()!.LayoutType
                : razorHxComponentsServiceOptions.RootComponent;
        }

        var parameters = new Dictionary<string, object?>
        {
            { "Layout", layout },
            { "ComponentType", ComponentType },
            { "Parameters", (Dictionary<string, object?>)Parameters.ToDictionary() },
            {
                "OutOfBandComponents", OutOfBandComponents
                    .Select(oob => (oob.Item1, (Dictionary<string, object?>)oob.Item2.ToDictionary())).ToList()
            },
        };

        var htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output =
                await htmlRenderer.RenderComponentAsync(typeof(HxLayout), ParameterView.FromDictionary(parameters));
            return output.ToHtmlString();
        });

        await httpContext.Response.WriteAsync(htmlContent);
    }

    private void PrepareResponseHeaders(HttpContext httpContext)
    {
        foreach (var trigger in Triggers)
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

    internal static ParameterView CoerceParametersObjectToDictionary(object? parameters)
    {
        return parameters is null
            ? throw new ArgumentNullException(nameof(parameters))
            : ParameterView.FromDictionary(parameters.ToDictionary());
    }
}
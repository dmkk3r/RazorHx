using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RazorHx.Components;
using RazorHx.DependencyInjection;
using RazorHx.Htmx.HttpContextFeatures;

namespace RazorHx.Results;

internal static class RazorHxResultExecutor
{
    private const string DefaultContentType = "text/html; charset=utf-8";

    public static Task ExecuteAsync(HttpContext httpContext, RazorHxResult result)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var response = httpContext.Response;
        response.ContentType = result.ContentType ?? DefaultContentType;

        if (result.StatusCode != null) response.StatusCode = result.StatusCode.Value;

        return RenderComponentToResponse(
            httpContext,
            result.ComponentType,
            result.Parameters,
            result.OobComponentType,
            result.OobParameters);
    }

    private static async Task<Task> RenderComponentToResponse(
        HttpContext httpContext,
        Type componentType,
        ParameterView componentParameters,
        Type? oobComponentType,
        ParameterView oobComponentParameters)
    {
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
            var useComponentLayout = componentType.GetCustomAttribute<LayoutAttribute>() != null;
            layout = useComponentLayout
                ? componentType.GetCustomAttribute<LayoutAttribute>()!.LayoutType
                : razorHxComponentsServiceOptions.RootComponent;
        }

        var parameters = new Dictionary<string, object?>
        {
            { "Layout", layout },
            { "ComponentType", componentType },
            { "Parameters", (Dictionary<string, object?>)componentParameters.ToDictionary() }
        };

        if (oobComponentType != null)
        {
            parameters.Add("OobComponentType", oobComponentType);
            parameters.Add("OobParameters", (Dictionary<string, object?>)oobComponentParameters.ToDictionary());
        }

        var htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output =
                await htmlRenderer.RenderComponentAsync(typeof(HxLayout), ParameterView.FromDictionary(parameters));
            return output.ToHtmlString();
        });

        return httpContext.Response.WriteAsync(htmlContent);
    }
}
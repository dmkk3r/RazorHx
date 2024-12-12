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
            result.Parameters);
    }

    private static async Task<Task> RenderComponentToResponse(HttpContext httpContext, Type componentType,
        ParameterView componentParameters)
    {
        var htmlRenderer = httpContext.RequestServices.GetRequiredService<HtmlRenderer>();
        var razorHxComponentsServiceOptions = httpContext.RequestServices.GetRequiredService<RazorHxServiceOptions>();

        var htmxRequestFeature = httpContext.Features.Get<IHtmxRequestFeature>();

        if (htmxRequestFeature == null)
            throw new InvalidOperationException("HtmxRequestFeature is null");

        string htmlContent;

        if (htmxRequestFeature.CurrentRequest.Request && !htmxRequestFeature.CurrentRequest.Boosted)
        {
            htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await htmlRenderer.RenderComponentAsync(componentType, componentParameters);
                return output.ToHtmlString();
            });
        }
        else
        {
            var useComponentLayout = componentType.GetCustomAttribute<LayoutAttribute>() != null;
            var layout = useComponentLayout
                ? componentType.GetCustomAttribute<LayoutAttribute>()!.LayoutType
                : razorHxComponentsServiceOptions.RootComponent;

            var parameters = new Dictionary<string, object?>
            {
                { "Layout", layout },
                { "ComponentType", componentType },
                { "Parameters", (Dictionary<string, object?>)componentParameters.ToDictionary() }
            };

            htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output =
                    await htmlRenderer.RenderComponentAsync(typeof(HxLayout), ParameterView.FromDictionary(parameters));
                return output.ToHtmlString();
            });
        }


        return httpContext.Response.WriteAsync(htmlContent);
    }
}
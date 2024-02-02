using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RazorHx.Components;
using RazorHx.Htmx.HttpContextFeatures;
using RazorHx.DependencyInjection;

namespace RazorHx.Results;

internal static class RazorHxResultExecutor {
    private const string DefaultContentType = "text/html; charset=utf-8";

    public static Task ExecuteAsync(HttpContext httpContext, RazorHxResult result) {
        ArgumentNullException.ThrowIfNull(httpContext);

        var response = httpContext.Response;
        response.ContentType = result.ContentType ?? DefaultContentType;

        if (result.StatusCode != null)
        {
            response.StatusCode = result.StatusCode.Value;
        }

        return RenderComponentToResponse(
            httpContext,
            result.ComponentType,
            result.Parameters);
    }

    private static async Task<Task> RenderComponentToResponse(HttpContext httpContext, Type componentType, ParameterView componentParameters) {
        var htmlRenderer = httpContext.RequestServices.GetRequiredService<HtmlRenderer>();
        var razorHxComponentsServiceOptions = httpContext.RequestServices.GetRequiredService<RazorHxServiceOptions>();

        var htmxRequestFeature = httpContext.Features.Get<IHtmxRequestFeature>();

        if (htmxRequestFeature == null)
            throw new InvalidOperationException("HtmxRequestFeature is null");

        string htmlContent;

        var isHtmxRequest = htmxRequestFeature.CurrentRequest.Request is "true";
        var isBoosted = htmxRequestFeature.CurrentRequest.Boosted is "true";

        if (isHtmxRequest && !isBoosted)
        {
            htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () => {
                var output = await htmlRenderer.RenderComponentAsync(componentType, componentParameters);
                return output.ToHtmlString();
            });
        }
        else
        {
            var parameters = new Dictionary<string, object?>
            {
                { "Layout", razorHxComponentsServiceOptions.RootComponent },
                { "ComponentType", componentType },
                { "Parameters", (Dictionary<string, object?>)componentParameters.ToDictionary() }
            };

            htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () => {
                var output = await htmlRenderer.RenderComponentAsync(typeof(HxLayout), ParameterView.FromDictionary(parameters));
                return output.ToHtmlString();
            });
        }


        return httpContext.Response.WriteAsync(htmlContent);
    }
}
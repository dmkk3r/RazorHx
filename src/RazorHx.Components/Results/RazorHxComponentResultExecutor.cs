using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.Components.Results;

internal static class RazorHxComponentResultExecutor {
    private const string DefaultContentType = "text/html; charset=utf-8";

    public static Task ExecuteAsync(HttpContext httpContext, RazorHxComponentResult result) {
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

        var htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () => {
            var output = await htmlRenderer.RenderComponentAsync(componentType, componentParameters);
            return output.ToHtmlString();
        });

        return httpContext.Response.WriteAsync(htmlContent);
    }
}
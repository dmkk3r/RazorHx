using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RazorHx.Components;
using RazorHx.Htmx.HttpContextFeatures;
using RazorHx.Results.Interfaces;

namespace RazorHx.Results;

public class RazorHxResult<TComponent, TType> : IRazorHxStreamResult where TComponent : IComponent
{
    public string ContentType { get; set; } = "text/event-stream";
    public Type ComponentType { get; }
    public List<(Type, ParameterView)> OutOfBandComponents { get; } = [];
    private IAsyncEnumerable<TType> Stream { get; }

    public RazorHxResult(IAsyncEnumerable<TType> stream)
    {
        ComponentType = typeof(TComponent);
        Stream = stream;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var htmlRenderer = httpContext.RequestServices.GetRequiredService<HtmlRenderer>();
        var htmxRequestFeature = httpContext.Features.Get<IHtmxRequestFeature>();

        if (htmxRequestFeature == null)
        {
            throw new InvalidOperationException("HtmxRequestFeature is null");
        }

        var response = httpContext.Response;
        response.ContentType = ContentType;
        response.StatusCode = response.StatusCode;

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
                : typeof(EmptyLayout);
        }

        var parameters = new Dictionary<string, object?>
        {
            { "Layout", layout },
            { "ComponentType", ComponentType },
            { "Parameters", ParameterView.Empty },
            {
                "OutOfBandComponents", OutOfBandComponents
                    .Select(oob => (oob.Item1, (Dictionary<string, object?>)oob.Item2.ToDictionary()))
                    .ToList()
            }
        };

        var propertyInfo = ComponentType.GetProperties()
            .FirstOrDefault(prop => prop.GetCustomAttribute<StreamParameterAttribute>() != null);

        if (propertyInfo == null)
        {
            throw new InvalidOperationException("No property with StreamParameter attribute found");
        }

        await foreach (var streamItem in Stream)
        {
            parameters["Parameters"] = new Dictionary<string, object?>
                { { propertyInfo.Name, streamItem } };

            var htmlContent = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await htmlRenderer.RenderComponentAsync(typeof(HxLayout),
                    ParameterView.FromDictionary(parameters));

                return output.ToHtmlString();
            });

            // Clean newlines to not break SSE newline message separation...
            // Won't work for <pre> tags -> https://github.com/bigskysoftware/htmx/issues/2292
            htmlContent = htmlContent.Replace("\n", string.Empty);

            await httpContext.Response.WriteAsync($"data: {htmlContent}");
            await httpContext.Response.WriteAsync("\n\n");
            await httpContext.Response.Body.FlushAsync();

            if (!httpContext.RequestAborted.IsCancellationRequested) continue;

            break;
        }

        await httpContext.Response.CompleteAsync();
    }
}
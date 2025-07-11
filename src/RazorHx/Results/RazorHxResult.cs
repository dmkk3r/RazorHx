using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using RazorHx.Extensions;
using RazorHx.Htmx;

namespace RazorHx.Results;

public class RazorHxResult : IResult, IStatusCodeHttpResult, IContentTypeHttpResult
{
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

    public string? ContentType { get; set; }
    public int? StatusCode { get; set; }
    public Type ComponentType { get; }
    public Type? OobComponentType { get; set; }
    public ParameterView Parameters { get; }
    public ParameterView OobParameters { get; set; }
    public List<HtmxTrigger> Triggers { get; } = [];

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        await RazorHxResultExecutor.ExecuteAsync(httpContext, this);
    }

    internal static ParameterView CoerceParametersObjectToDictionary(object? parameters)
    {
        return parameters is null
            ? throw new ArgumentNullException(nameof(parameters))
            : ParameterView.FromDictionary(parameters.ToDictionary());
    }
}
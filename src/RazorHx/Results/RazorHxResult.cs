using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace RazorHx.Results;

public class RazorHxResult : IResult, IStatusCodeHttpResult, IContentTypeHttpResult
{
    private static readonly ParameterView EmptyParameters = ParameterView.Empty;

    public RazorHxResult(Type componentType) : this(componentType, EmptyParameters)
    {
    }

    public RazorHxResult(Type componentType, object parameters)
        : this(componentType, CoerceParametersObjectToDictionary(parameters))
    {
    }

    private RazorHxResult(Type componentType, ParameterView parameters)
    {
        ArgumentNullException.ThrowIfNull(componentType);

        ComponentType = componentType;
        Parameters = parameters;
    }

    public Type ComponentType { get; }

    public ParameterView Parameters { get; }

    public string? ContentType { get; set; }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        return RazorHxResultExecutor.ExecuteAsync(httpContext, this);
    }

    public int? StatusCode { get; set; }

    private static ParameterView CoerceParametersObjectToDictionary(object? parameters)
    {
        return parameters is null
            ? throw new ArgumentNullException(nameof(parameters))
            : ParameterView.FromDictionary(parameters.ToDictionary());
    }
}
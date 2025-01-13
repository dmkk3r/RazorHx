using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace RazorHx.Results;

public class RazorHxResult : IResult, IStatusCodeHttpResult, IContentTypeHttpResult
{
    internal static readonly ParameterView EmptyParameters = ParameterView.Empty;

    protected RazorHxResult(Type componentType)
        : this(componentType, EmptyParameters)
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
    public Type? OobComponentType { get; protected set; }
    public ParameterView Parameters { get; }
    public ParameterView OobParameters { get; protected set; }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        return RazorHxResultExecutor.ExecuteAsync(httpContext, this);
    }

    internal static ParameterView CoerceParametersObjectToDictionary(object? parameters)
    {
        return parameters is null
            ? throw new ArgumentNullException(nameof(parameters))
            : ParameterView.FromDictionary(parameters.ToDictionary());
    }
}
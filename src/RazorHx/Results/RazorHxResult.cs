using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace RazorHx.Results;

public class RazorHxResult : IResult, IStatusCodeHttpResult, IContentTypeHttpResult {
    private static readonly ParameterView EmptyParameters = ParameterView.Empty;

    public RazorHxResult(Type componentType) : this(componentType, EmptyParameters) { }

    public RazorHxResult(Type componentType, object parameters)
        : this(componentType, CoerceParametersObjectToDictionary(parameters)) { }

    private RazorHxResult(Type componentType, ParameterView parameters) {
        ArgumentNullException.ThrowIfNull(componentType);

        ComponentType = componentType;
        Parameters = parameters;
    }

    private static ParameterView CoerceParametersObjectToDictionary(object? parameters)
        => parameters is null
            ? throw new ArgumentNullException(nameof(parameters))
            : ParameterView.FromDictionary(parameters.ToDictionary());

    public Type ComponentType { get; }

    public string? ContentType { get; set; }

    public int? StatusCode { get; set; }

    public ParameterView Parameters { get; }

    public Task ExecuteAsync(HttpContext httpContext)
        => RazorHxResultExecutor.ExecuteAsync(httpContext, this);
}
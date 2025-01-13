using Microsoft.AspNetCore.Components;

namespace RazorHx.Results;

public class RazorHxResult<T> : RazorHxResult where T : IComponent
{
    public RazorHxResult() : base(typeof(T))
    {
    }

    public RazorHxResult(object parameters) : base(typeof(T), parameters)
    {
    }

    public RazorHxResult(IReadOnlyDictionary<string, object?> parameters) :
        base(typeof(T), parameters)
    {
    }

    public RazorHxResult<T> WithOutOfBand<TComponent>() where TComponent : IComponent
    {
        OobComponentType = typeof(TComponent);
        OobParameters = EmptyParameters;
        return this;
    }

    public RazorHxResult<T> WithOutOfBand<TComponent>(object parameters) where TComponent : IComponent
    {
        OobComponentType = typeof(TComponent);
        OobParameters = CoerceParametersObjectToDictionary(parameters);
        return this;
    }
}
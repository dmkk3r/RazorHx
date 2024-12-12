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
}
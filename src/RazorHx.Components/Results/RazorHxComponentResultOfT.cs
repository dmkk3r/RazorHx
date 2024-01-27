using Microsoft.AspNetCore.Components;

namespace RazorHx.Components.Results;

public class RazorHxComponentResult<T> : RazorHxComponentResult where T : IComponent {
    public RazorHxComponentResult() : base(typeof(T)) { }

    public RazorHxComponentResult(object parameters) : base(typeof(T), parameters) { }

    public RazorHxComponentResult(IReadOnlyDictionary<string, object?> parameters) :
        base(typeof(T), parameters) { }
}
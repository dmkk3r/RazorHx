using Microsoft.AspNetCore.Components;
using RazorHx.Htmx;

namespace RazorHx.Results;

public static class RazorHxResultExtensions
{
    public static RazorHxResult WithTrigger(this RazorHxResult result, string @event,
        bool include = true, object? parameters = null, TriggerTiming timing = TriggerTiming.Default)
    {
        if (!include)
            return result;

        result.Triggers.Add(new HtmxTrigger
        {
            Name = @event,
            Timing = timing,
            Parameters = parameters
        });

        return result;
    }

    public static RazorHxResult WithOutOfBand<TComponent>(this RazorHxResult result, object? parameters = null)
        where TComponent : IComponent
    {
        result.OobComponentType = typeof(TComponent);
        result.OobParameters = parameters == null
            ? ParameterView.Empty
            : RazorHxResult.CoerceParametersObjectToDictionary(parameters);

        return result;
    }
}
using Microsoft.AspNetCore.Components;
using RazorHx.Htmx;
using RazorHx.Results.Interfaces;

namespace RazorHx.Results.Extensions;

public static class RazorHxResultExtensions
{
    public static IRazorHxResult WithTrigger(this IRazorHxResult result, string @event,
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

    public static IRazorHxResult WithOutOfBand<TComponent>(this IRazorHxResult result, object? parameters = null)
        where TComponent : IComponent
    {
        result.OutOfBandComponents.Add((typeof(TComponent), parameters == null
            ? ParameterView.Empty
            : RazorHxResult.CoerceParametersObjectToDictionary(parameters)));

        return result;
    }

    public static IRazorHxStreamResult WithOutOfBand<TComponent>(
        this IRazorHxStreamResult result, object? parameters = null)
        where TComponent : IComponent
    {
        result.OutOfBandComponents.Add((typeof(TComponent), parameters == null
            ? ParameterView.Empty
            : RazorHxResult.CoerceParametersObjectToDictionary(parameters)));

        return result;
    }
}
namespace RazorHx.Htmx;

public class HtmxTrigger
{
    public required string Name { get; init; }
    public TriggerTiming Timing { get; init; }
    public object? Parameters { get; init; }
}
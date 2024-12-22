using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx;

public class HtmxRequest(HttpRequest request)
{
    private HttpRequest HttpRequest { get; } = request;

    public bool Boosted => HttpRequest.Headers[HtmxRequestHeaderKeys.Boosted] == "true";
    public string? CurrentUrl => HttpRequest.Headers[HtmxRequestHeaderKeys.CurrentUrl];
    public bool HistoryRestoreRequest => HttpRequest.Headers[HtmxRequestHeaderKeys.HistoryRestoreRequest] == "true";
    public string? Prompt => HttpRequest.Headers[HtmxRequestHeaderKeys.Prompt];
    public bool Request => HttpRequest.Headers[HtmxRequestHeaderKeys.Request] == "true";
    public string? Target => HttpRequest.Headers[HtmxRequestHeaderKeys.Target];
    public string? TriggerName => HttpRequest.Headers[HtmxRequestHeaderKeys.TriggerName];
    public string? Trigger => HttpRequest.Headers[HtmxRequestHeaderKeys.Trigger];
}
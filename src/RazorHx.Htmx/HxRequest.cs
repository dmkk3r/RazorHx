using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx;

public class HxRequest(HttpRequest request)
{
    private HttpRequest HttpRequest { get; } = request;

    public bool Boosted => HttpRequest.Headers[HxRequestHeaderKeys.Boosted] == "true";
    public string? CurrentUrl => HttpRequest.Headers[HxRequestHeaderKeys.CurrentUrl];
    public bool HistoryRestoreRequest => HttpRequest.Headers[HxRequestHeaderKeys.HistoryRestoreRequest] == "true";
    public string? Prompt => HttpRequest.Headers[HxRequestHeaderKeys.Prompt];
    public bool Request => HttpRequest.Headers[HxRequestHeaderKeys.Request] == "true";
    public string? Target => HttpRequest.Headers[HxRequestHeaderKeys.Target];
    public string? TriggerName => HttpRequest.Headers[HxRequestHeaderKeys.TriggerName];
    public string? Trigger => HttpRequest.Headers[HxRequestHeaderKeys.Trigger];
}
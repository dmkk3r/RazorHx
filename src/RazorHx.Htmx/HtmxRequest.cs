using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx;

public class HtmxRequest(HttpRequest request) {
    private HttpRequest HttpRequest { get; } = request;

    public string? Boosted
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.Boosted];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.Boosted] = value;
    }

    public string? CurrentUrl
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.CurrentUrl];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.CurrentUrl] = value;
    }

    public string? HistoryRestoreRequest
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.HistoryRestoreRequest];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.HistoryRestoreRequest] = value;
    }

    public string? Prompt
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.Prompt];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.Prompt] = value;
    }

    public string? Request
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.Request];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.Request] = value;
    }

    public string? Target
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.Target];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.Target] = value;
    }

    public string? TriggerName
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.TriggerName];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.TriggerName] = value;
    }

    public string? Trigger
    {
        get => HttpRequest.Headers[HtmxRequestHeaderKeys.Trigger];
        set => HttpRequest.Headers[HtmxRequestHeaderKeys.Trigger] = value;
    }
}
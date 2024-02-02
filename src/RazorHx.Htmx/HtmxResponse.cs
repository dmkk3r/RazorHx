using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx;

public class HtmxResponse(HttpResponse response) {
    private HttpResponse HttpResponse { get; } = response;

    public string? Location
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Location];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Location] = value;
    }

    public string? PushUrl
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.PushUrl];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.PushUrl] = value;
    }

    public string? Redirect
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Redirect];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Redirect] = value;
    }

    public string? RefreshUrl
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Refresh];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Refresh] = value;
    }

    public string? ReplaceUrl
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.ReplaceUrl];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.ReplaceUrl] = value;
    }

    public string? Reswap
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Reswap];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Reswap] = value;
    }

    public string? Retarget
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Retarget];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Retarget] = value;
    }

    public string? Reselect
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Reselect];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Reselect] = value;
    }

    public string? Trigger
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.Trigger];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.Trigger] = value;
    }

    public string? TriggerAfterSettle
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.TriggerAfterSettle];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.TriggerAfterSettle] = value;
    }

    public string? TriggerAfterSwap
    {
        get => HttpResponse.Headers[HtmxResponseHeaderKeys.TriggerAfterSwap];
        set => HttpResponse.Headers[HtmxResponseHeaderKeys.TriggerAfterSwap] = value;
    }
}
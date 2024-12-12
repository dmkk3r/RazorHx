using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx;

public class HtmxResponse(HttpResponse response)
{
    private readonly IHeaderDictionary _headers;
    private HttpResponse HttpResponse { get; } = response;

    public HtmxResponse Location(string? value)
    {
        _headers[HtmxResponseHeaderKeys.Location] = value;
        return this;
    }

    public HtmxResponse PushUrl(string? value)
    {
        _headers[HtmxResponseHeaderKeys.PushUrl] = value;
        return this;
    }

    public HtmxResponse Redirect(string? value)
    {
        _headers[HtmxResponseHeaderKeys.Redirect] = value;
        return this;
    }

    public HtmxResponse Refresh()
    {
        _headers[HtmxResponseHeaderKeys.Refresh] = true.ToString();
        return this;
    }

    public HtmxResponse ReplaceUrl(string? value)
    {
        _headers[HtmxResponseHeaderKeys.ReplaceUrl] = value;
        return this;
    }

    public HtmxResponse Reswap(string? value)
    {
        _headers[HtmxResponseHeaderKeys.Reswap] = value;
        return this;
    }

    public HtmxResponse Retarget(string? value)
    {
        _headers[HtmxResponseHeaderKeys.Retarget] = value;
        return this;
    }

    public HtmxResponse Reselect(string? value)
    {
        _headers[HtmxResponseHeaderKeys.Reselect] = value;
        return this;
    }

    public HtmxResponse Trigger(string? value)
    {
        _headers[HtmxResponseHeaderKeys.Trigger] = value;
        return this;
    }

    public HtmxResponse TriggerAfterSettle(string? value)
    {
        _headers[HtmxResponseHeaderKeys.TriggerAfterSettle] = value;
        return this;
    }

    public HtmxResponse TriggerAfterSwap(string? value)
    {
        _headers[HtmxResponseHeaderKeys.TriggerAfterSwap] = value;
        return this;
    }

    public void WriteHeaders()
    {
        foreach (var (key, value) in _headers) HttpResponse.Headers[key] = value;
    }
}
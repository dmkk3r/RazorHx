using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx;

public class HxResponse(HttpResponse response)
{
    private readonly IHeaderDictionary _headers;
    private HttpResponse HttpResponse { get; } = response;

    public HxResponse Location(string? value)
    {
        _headers[HxResponseHeaderKeys.Location] = value;
        return this;
    }

    public HxResponse PushUrl(string? value)
    {
        _headers[HxResponseHeaderKeys.PushUrl] = value;
        return this;
    }

    public HxResponse Redirect(string? value)
    {
        _headers[HxResponseHeaderKeys.Redirect] = value;
        return this;
    }

    public HxResponse Refresh()
    {
        _headers[HxResponseHeaderKeys.Refresh] = true.ToString();
        return this;
    }

    public HxResponse ReplaceUrl(string? value)
    {
        _headers[HxResponseHeaderKeys.ReplaceUrl] = value;
        return this;
    }

    public HxResponse Reswap(string? value)
    {
        _headers[HxResponseHeaderKeys.Reswap] = value;
        return this;
    }

    public HxResponse Retarget(string? value)
    {
        _headers[HxResponseHeaderKeys.Retarget] = value;
        return this;
    }

    public HxResponse Reselect(string? value)
    {
        _headers[HxResponseHeaderKeys.Reselect] = value;
        return this;
    }

    public HxResponse Trigger(string? value)
    {
        _headers[HxResponseHeaderKeys.Trigger] = value;
        return this;
    }

    public HxResponse TriggerAfterSettle(string? value)
    {
        _headers[HxResponseHeaderKeys.TriggerAfterSettle] = value;
        return this;
    }

    public HxResponse TriggerAfterSwap(string? value)
    {
        _headers[HxResponseHeaderKeys.TriggerAfterSwap] = value;
        return this;
    }

    public void WriteHeaders()
    {
        foreach (var (key, value) in _headers) HttpResponse.Headers[key] = value;
    }
}
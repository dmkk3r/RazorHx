using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    ///     Returns true if the request is an HTMX request.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static bool IsHxRequest(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).Request;
    }

    /// <summary>
    ///     Returns true if the request is a boosted HTMX request.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static bool IsHxBoosted(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).Boosted;
    }

    /// <summary>
    ///     Returns true if the request is for history restoration after a miss in the local history cache.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static bool IsHxHistoryRestoreRequest(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).HistoryRestoreRequest;
    }

    /// <summary>
    ///     Returns the current URL of the browser.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static string? HxCurrentUrl(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).CurrentUrl;
    }

    /// <summary>
    ///     Returns the user response to an hx-prompt.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static string? HxPrompt(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).Prompt;
    }

    /// <summary>
    ///     Returns the id of the target element if it exists.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static string? HxTarget(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).Target;
    }

    /// <summary>
    ///     Returns the id of the triggered element if it exists.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static string? HxTrigger(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).Trigger;
    }

    /// <summary>
    ///     Returns the name of the triggered element if it exists.
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns></returns>
    public static string? HxTriggerName(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return new HxRequest(request).TriggerName;
    }
}
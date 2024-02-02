using Microsoft.AspNetCore.Http;
using RazorHx.Htmx.HttpContextFeatures;

namespace RazorHx.Htmx.Middlewares;

public class HtmxRequestMiddleware {
    private readonly RequestDelegate _next;

    public HtmxRequestMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        var htmxRequestFeature = new HtmxRequestFeature(context.Request);
        context.Features.Set<IHtmxRequestFeature>(htmxRequestFeature);

        await _next(context);
    }
}
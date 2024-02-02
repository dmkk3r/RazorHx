using Microsoft.AspNetCore.Builder;
using RazorHx.Htmx.Middlewares;

namespace RazorHx.Builder;

public static class RazorHxApplicationBuilderExtensions {
    public static void UseRazorHxComponents(this IApplicationBuilder application) {
        ArgumentNullException.ThrowIfNull(application);

        application.UseMiddleware<HtmxRequestMiddleware>();
    }
}
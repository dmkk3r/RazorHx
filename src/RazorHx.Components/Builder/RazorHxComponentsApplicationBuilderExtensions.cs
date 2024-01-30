using Microsoft.AspNetCore.Builder;
using RazorHx.Components.Htmx.Middlewares;

namespace RazorHx.Components.Builder;

public static class RazorHxComponentsApplicationBuilderExtensions {
    public static void UseRazorHxComponents(this IApplicationBuilder application) {
        ArgumentNullException.ThrowIfNull(application);

        application.UseMiddleware<HtmxRequestMiddleware>();
    }
}
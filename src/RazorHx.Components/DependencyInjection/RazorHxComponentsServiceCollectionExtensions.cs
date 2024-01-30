using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.Components.DependencyInjection;

public static class RazorHxComponentsServiceCollectionExtensions {
    public static IRazorHxComponentsBuilder AddRazorHxComponents(this IServiceCollection services,
        Action<RazorHxComponentsServiceOptions>? configure = null) {
        ArgumentNullException.ThrowIfNull(services);

        var options = new RazorHxComponentsServiceOptions();
        configure?.Invoke(options);
        
        services.AddSingleton(options);

        services.AddSingleton<RazorHxComponentsMarkerService>();

        services.AddLogging();
        services.AddSingleton<HtmlRenderer>();

        return new DefaultRazorComponentsBuilder(services);
    }
}
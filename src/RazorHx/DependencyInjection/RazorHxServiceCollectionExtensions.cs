using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.DependencyInjection;

public static class RazorHxServiceCollectionExtensions
{
    public static IRazorHxBuilder AddRazorHxComponents(this IServiceCollection services,
        Action<RazorHxServiceOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var options = new RazorHxServiceOptions();
        configure?.Invoke(options);

        services.AddSingleton(options);

        services.AddSingleton<RazorHxMarkerService>();

        services.AddLogging();
        services.AddSingleton<HtmlRenderer>();

        return new DefaultRazorHxBuilder(services);
    }
}
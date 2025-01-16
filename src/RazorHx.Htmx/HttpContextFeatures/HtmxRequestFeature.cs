using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx.HttpContextFeatures;

public class HtmxRequestFeature(HttpRequest request) : IHtmxRequestFeature
{
    public HxRequest CurrentRequest { get; set; } = new(request);
}
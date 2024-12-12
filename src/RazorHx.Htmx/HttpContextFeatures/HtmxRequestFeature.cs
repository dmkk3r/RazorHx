using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx.HttpContextFeatures;

public class HtmxRequestFeature : IHtmxRequestFeature
{
    public HtmxRequestFeature(HttpRequest request)
    {
        CurrentRequest = new HtmxRequest(request);
    }

    public HtmxRequest CurrentRequest { get; set; }
}
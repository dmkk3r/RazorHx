using Microsoft.AspNetCore.Http;

namespace RazorHx.Htmx.HttpContextFeatures;

public class HtmxRequestFeature : IHtmxRequestFeature {
    public HtmxRequest CurrentRequest { get; set; }

    public HtmxRequestFeature(HttpRequest request) {
        CurrentRequest = new HtmxRequest(request);
    }
}
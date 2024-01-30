namespace RazorHx.Components.Htmx.HttpContextFeatures;

public interface IHtmxRequestFeature {
    HtmxRequest CurrentRequest { get; set; }
}
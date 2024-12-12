namespace RazorHx.Htmx.HttpContextFeatures;

public interface IHtmxRequestFeature
{
    HtmxRequest CurrentRequest { get; set; }
}
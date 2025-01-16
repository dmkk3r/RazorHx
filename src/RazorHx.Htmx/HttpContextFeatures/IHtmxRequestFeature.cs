namespace RazorHx.Htmx.HttpContextFeatures;

public interface IHtmxRequestFeature
{
    HxRequest CurrentRequest { get; set; }
}
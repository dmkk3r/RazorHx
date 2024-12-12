using Microsoft.AspNetCore.Components;
using RazorHx.Components;

namespace RazorHx.Sample.Components;

public partial class Get : HxComponent
{
    [Inject] private ILogger<Get> _logger { get; set; } = null!;

    [Parameter]
    public SampleModel Model { get; set; } = new()
    {
        Email = "Max.Mustermann@muster.de",
        Name = "Max Mustermann",
        Password = "Secret123!",
        Age = 45
    };

    protected override void OnInitialized()
    {
        _logger.LogInformation("OnInitialized");
    }
}
# RazorHx

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

This small helper library seamlessly combines HTMX with the latest Razor Components in the ASP.NET Core world. With this
library, you can effortlessly return HTML through Minimal API endpoints, which is then rendered by Razor Components.
Streamline development by harnessing the power of HTMX alongside the flexibility of Razor Components and Minimal APIs.

## Features

- Seamless integration of HTMX with Razor Components.
- Return HTML rendered by Razor Components via Minimal API endpoints.
- Accelerate the development of dynamic web applications in ASP.NET Core.

## Installation

To use RazorHx in your ASP.NET Core project, you only need to install the NuGet package. Open your project in the
Package Manager Console and run the following command:

```bash
Install-Package RazorHx
```

Alternatively, you can use the .NET CLI with the following command:

```bash
dotnet add package RazorHx
```

## Usage

Here's a quick example of how to use the library:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorHxComponents(options => {
    options.RootComponent = typeof(RootLayout);
});

var app = builder.Build();

app.UseRazorHxComponents();

app.MapGet("/", () => new RazorHxResult<Hello>());

app.Run();
```

### Triggers

[Read more at the official documentation site.](https://htmx.org/docs/#triggers)

Create an endpoint which returns a RazorHxResult and append the WithTrigger extension to specify the event to trigger:

```csharp
app.MapGet("/", () => new RazorHxResult<Hello>()
    .WithTrigger("event"));
```

Specify the timing, when the event should trigger:

```csharp
app.MapGet("/", () => new RazorHxResult<Hello>()
    .WithTrigger("event", timing: TriggerTiming.Default)
    .WithTrigger("event", timing: TriggerTiming.AfterSettle)
    .WithTrigger("event", timing: TriggerTiming.AfterSwap));
```

Specify the condition, when the event should be included:

```csharp
app.MapGet("/", () => new RazorHxResult<Hello>()
    .WithTrigger("event", include: false));
```

### Out of Band Swaps

[Read more at the official documentation site.](https://htmx.org/docs/#oob_swaps)

Create an endpoint which returns a RazorHxResult and append the WithOutOfBand extension to specify your oob component:

```csharp
app.MapGet("/", () => new RazorHxResult<Hello>()
    .WithOutOfBand<World>());
```

Define a slot in the DOM where the response will be swapped to:

```html
<!-- Hello.razor or somewhere in Layouts -->
<div id="oob">
    ...
</div>
```

Lastly create a component which holds the content to swap in:

```html
<!-- World.razor -->
<div id="oob" hx-swap-oob="true">
    ...
</div>
```

### Server Sent Events

[Read more at the official documentation site.](https://htmx.org/extensions/sse/)

Create an endpoint which returns a RazorHxResult and provide an IAsyncEnumerable:

```csharp
var channel = Channel.CreateUnbounded<int>();

app.MapGet("/sse", () => new RazorHxResult<Sse, int>(channel.Reader.ReadAllAsync()) );
```

The generic parameters on this result specify the type of component and parameter which will be updated each iteration.
Mark the parameter with the StreamParameter attribute:

```razor
@using RazorHx.Results

<p>Count: @Count</p>

@code {
    [Parameter] [StreamParameter] public int Count { get; set; }
}
```

## Changelog

For a detailed changelog, please refer to the [CHANGELOG.md](CHANGELOG.md) file.

## Contributing

We welcome contributions and feedback on these planned features. If you have specific features you'd like to see or
would like to contribute to the development, please check our [contribution guidelines](CONTRIBUTING.md)

## License

This project is licensed under the MIT License.

Note: This library might still be in the development phase. We welcome your contributions and feedback!

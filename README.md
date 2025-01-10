# RazorHx

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

This small helper library seamlessly combines HTMX with the latest Razor Components in the ASP.NET Core world. With this library, you can effortlessly return HTML through Minimal API endpoints, which is then rendered by Razor Components. Streamline development by harnessing the power of HTMX alongside the flexibility of Razor Components and Minimal APIs.

## Features

- Seamless integration of HTMX with Razor Components.
- Return HTML rendered by Razor Components via Minimal API endpoints.
- Accelerate the development of dynamic web applications in ASP.NET Core.

## Installation

To use RazorHx in your ASP.NET Core project, you only need to install the NuGet package. Open your project in the Package Manager Console and run the following command:
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

## Changelog

For a detailed changelog, please refer to the [CHANGELOG.md](CHANGELOG.md) file.

## Contributing

We welcome contributions and feedback on these planned features. If you have specific features you'd like to see or would like to contribute to the development, please check our [contribution guidelines](CONTRIBUTING.md)

## License

This project is licensed under the MIT License.

Note: This library might still be in the development phase. We welcome your contributions and feedback!

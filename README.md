# AppShapes.Logging.Console

![](https://github.com/appshapes-org/logging-console/workflows/Integration/badge.svg)

## What does this project do?

AppShapes.Logging.Console is a [Microsoft.Extensions.Logging.ILoggerProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.iloggerprovider) that streams log events to standard output. Default behaviors such as formatting, properties, and processing may be replaced by injecting your own interface implementation.

## Why is this project useful?

AppShapes.Logging.Console is a highly configurable and highly performant console provider.

## How do I get started?

To use AppShapes.Logging.Console you must install the [NuGet package](https://nuget.org/packages/AppShapes.Logging.Console) and then add the provider.

### Installation

```powershell
Install-Package AppShapes.Logging.Console -ProjectName <Startup Project Name>
```

### Configuration

In your `ConfigureServices` method:

```csharp
services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConfiguration(configuration.GetSection("Logging"));
    logging.AddConsoleLogger();
    // Add other providers as needed
});
```

### Testing

AppShapes.Logging.Console pull requests are checked for code coverage. You can run the tests using .NET CLI:

```bash
dotnet test
```

AppShapes.Logging.ConsoleTests integrates with [Coverlet](https://github.com/tonerdo/coverlet) so if you have it installed you can generate coverage reports. For example:

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=OpenCover /p:Exclude=\"[xunit.*]*\"  /p:ExcludeByAttribute=\"Obsolete,GeneratedCodeAttribute,CompilerGeneratedAttribute\"
```

### Overriding

* ILoggerProviderPatterner
* ILoggerProviderFormatter
* ILoggerProviderProcessor

## Where can I get more help, if I need it?

### Information

* [Logging in .NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/)

### Issues

* AppShapes.Logging.Console [Issues](https://github.com/appshapes-org/logging-console/issues)

## Contributing

Have an idea for an improvement? See an issue you want to fix? Start by commenting on an existing [issue][issue_list] or creating a new one. Once you have an issue to reference, create a branch named after the issue. When your improvement or fix is ready submit a PR for review. We closely monitor the issue tracker and pull requests.

For more details check out our [contributing guide](CONTRIBUTING.md). When contributing please keep in mind our [Code of Conduct](CODE_OF_CONDUCT.md).

## Authors

* [Rjae Easton](https://github.com/Rjae)

## License

This project is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html).
# ResultDotNet.AspNetCore

Extensions for using the Result Pattern in ASP.NET Core applications.

## Overview
ResultDotNet.AspNetCore provides helpers and extension methods to integrate the Result Pattern with ASP.NET Core, making it easier to handle errors, map results to HTTP responses, and compose functional pipelines in web APIs.

## Installation
Add the package via NuGet:

```shell
# .NET CLI
 dotnet add package KrzysKlein.ResultDotNet.AspNetCore
```

## Basic Usage

### Mapping Results to HTTP Responses
```csharp
public IActionResult MyAction()
{
    var result = SomeService.DoSomething();
    return result.ToActionResult();
}
```

### Asynchronous Results
```csharp
public async Task<IActionResult> MyAsyncAction()
{
    var result = await SomeService.DoSomethingAsync();
    return result.ToActionResult();
}
```

### Custom Error Mapping
You can map errors to custom `ProblemDetails` or other response types using provided extension methods.

## API Overview
- `ToActionResult()`: Converts a `Result` or `Result<TValue, TError>` to an `IActionResult`.
- Extensions for handling `Task<Result<...>>` in async controller actions.
- Helpers for mapping errors to `ProblemDetails`.

## Examples
See [examples](../ResultDotNet.Examples.AspNetCoreApi/) for usage in real ASP.NET Core APIs.

## License
MIT License

## Links
- [NuGet Package](https://www.nuget.org/packages/KrzysKlein.ResultDotNet.AspNetCore)
- [GitHub Repository](https://github.com/krzysklein/result-dotnet)

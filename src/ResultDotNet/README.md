# ResultDotNet

Implementation of the Result Pattern for .NET 10

## Overview
ResultDotNet provides a functional approach to error handling and composition in .NET applications. It enables chaining operations, propagating errors, and handling results in a concise and expressive manner.

## Installation
Add the package via NuGet:

```shell
# .NET CLI
 dotnet add package KrzysKlein.ResultDotNet
```

## Basic Usage

### Creating Results
```csharp
var success = Result<string>.Success();
var error = Result<string>.FromError("Something went wrong");

var valueResult = Result<int, string>.FromValue(42);
var errorResult = Result<int, string>.FromError("Invalid value");
```

### Chaining Operations
```csharp
var result = Result<string>.Success()
    .Bind(() => Result<string>.FromError("Next step failed"))
    .Map(() => "Transformed value");
```

### Handling Results
```csharp
var output = result.Match(
    onSuccess: () => "Operation succeeded",
    onError: error => $"Failed: {error}"
);
```

## API Overview
- `Bind`: Chains operations that return results, propagating errors.
- `Map`: Transforms the value in a successful result.
- `Match`: Handles both success and error cases.
- Async variants: `BindAsync`, `MapAsync`, `MatchAsync` for asynchronous workflows.

## Examples
See [examples](./ResultDotNet.Examples.AspNetCoreApi/) for usage in ASP.NET Core APIs.

## License
MIT License

## Links
- [NuGet Package](https://www.nuget.org/packages/KrzysKlein.ResultDotNet)
- [GitHub Repository](https://github.com/krzysklein/result-dotnet)

# ResultDotNet.FluentValidation

FluentValidation integration for the Result Pattern in .NET 10.

## Overview
ResultDotNet.FluentValidation provides extension methods to easily convert FluentValidation results into ResultDotNet results, enabling seamless error handling and validation in functional pipelines.

## Installation
Add the package via NuGet:

```shell
# .NET CLI
 dotnet add package KrzysKlein.ResultDotNet.FluentValidation
```

## Basic Usage

### Converting Validation Results
```csharp
var validator = new MyValidator();
var validationResult = validator.Validate(model);
var result = validationResult.ToResult();
```

### Using with Result Pipelines
```csharp
var result = validator.Validate(model).ToResult()
    .Bind(() => DoSomethingIfValid());
```

## API Overview
- `ToResult()`: Converts a `FluentValidation.Results.ValidationResult` to a `Result<ProblemDetails>`.
- `ToValueResult()`: Converts a `FluentValidation.Results.ValidationResult` to a `ValueResult<ProblemDetails>`.
- Extension methods for integrating validation into functional result pipelines.

## Examples
See [examples](../ResultDotNet.Examples.AspNetCoreApi/) for usage in ASP.NET Core APIs.

## License
MIT License

## Links
- [NuGet Package](https://www.nuget.org/packages/KrzysKlein.ResultDotNet.FluentValidation)
- [GitHub Repository](https://github.com/krzysklein/result-dotnet)

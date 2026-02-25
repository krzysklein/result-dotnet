# result-dotnet

[![NuGet](https://img.shields.io/nuget/v/KrzysKlein.ResultDotNet.svg)](https://www.nuget.org/packages/KrzysKlein.ResultDotNet)
[![NuGet Downloads](https://img.shields.io/nuget/dt/KrzysKlein.ResultDotNet.svg)](https://www.nuget.org/packages/KrzysKlein.ResultDotNet)

Implementation of a Result Pattern in C#.

## Overview

`result-dotnet` provides a simple and robust implementation of the Result Pattern for C#. This pattern helps you handle success and failure cases in your code without relying on exceptions, making your code cleaner and easier to maintain.

## Features
- Strongly typed success and error handling
- Avoids exception-based flow control
- Easy integration into existing projects
- Supports custom error types

## Installation

Install via NuGet:

```shell
Install-Package KrzysKlein.ResultDotNet
```

Or using .NET CLI:

```shell
dotnet add package KrzysKlein.ResultDotNet
```

## Usage

```csharp
using ResultDotNet;

var success = Result<string>.Success();
var error = Result<string>.FromError("Something went wrong");

var valueResult = Result<int, string>.FromValue(42);
var errorResult = Result<int, string>.FromError("Invalid value");
```

## Example

```csharp
public Result<int, string> Divide(int numerator, int denominator)
{
    if (denominator == 0)
        return Result.FromError<int, string>("Division by zero.");
    return Result.FromValue<int, string>(numerator / denominator);
}
```

## Contributing

Contributions are welcome! Please open issues or pull requests for improvements or bug fixes.

## License

This project is licensed under the MIT License.

---

> Powered by [result-dotnet](https://github.com/krzysklein/result-dotnet)

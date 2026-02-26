# Changelog

## 0.2.2 - Bare Result and ValueResult types
- `Result` and `ValueResult` (without value and/or error) types implemented

## 0.2.1 - ValueResult
- `ValueResult<TError>` and `ValueResult<TValue, TError>` types implemented
- Added benchmarks comparing `Result<>` and `ValueResult<>` types

## 0.1.0 - Initial release
- `Result<TError>` and `Result<TValue, TError>` types implemented
- `Bind`, `Map` and `Match` methods added for functional composition + their `Async` versions
- ASP.NET Core extensions for mapping `Result<TError>` and `Result<TValue, TError>` types to `IActionResult`
- FluentValidation extensions for mapping `ValidationResult` to `Result<ProblemDetails>`
- Example ASP.NET Core Web API project created to demonstrate usage of the library
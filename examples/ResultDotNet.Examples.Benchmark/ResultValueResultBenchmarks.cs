using BenchmarkDotNet.Attributes;

namespace ResultDotNet.Examples.Benchmark;

public class ResultValueResultBenchmarks
{
#pragma warning disable CA1822 // Mark members as static

    [Benchmark]
    public Result<string, string> CreateResultSuccess() => Result<string, string>.FromValue("ok");

    [Benchmark]
    public Result<string, string> CreateResultError() => Result<string, string>.FromError("fail");

    [Benchmark]
    public string AccessResultValue()
    {
        var result = Result<string, string>.FromValue("ok");
        return result.Value;
    }

    [Benchmark]
    public string AccessResultError()
    {
        var result = Result<string, string>.FromError("fail");
        return result.Error;
    }

    [Benchmark]
    public ValueResult<string, string> CreateValueResultSuccess() => ValueResult<string, string>.FromValue("ok");

    [Benchmark]
    public ValueResult<string, string> CreateValueResultError() => ValueResult<string, string>.FromError("fail");

    [Benchmark]
    public string AccessValueResultValue()
    {
        var result = ValueResult<string, string>.FromValue("ok");
        return result.Value;
    }

    [Benchmark]
    public string AccessValueResultError()
    {
        var result = ValueResult<string, string>.FromError("fail");
        return result.Error;
    }

    [Benchmark]
    public Result<string, string> ResultBindSuccess()
    {
        var result = Result<string, string>.FromValue("ok");
        return result.Bind(v => Result<string, string>.FromValue("bound"));
    }

    [Benchmark]
    public Result<string, string> ResultBindError()
    {
        var result = Result<string, string>.FromError("fail");
        return result.Bind(v => Result<string, string>.FromValue("bound"));
    }

    [Benchmark]
    public Result<int, string> ResultMapSuccess()
    {
        var result = Result<string, string>.FromValue("ok");
        return result.Map(v => 42);
    }

    [Benchmark]
    public Result<int, string> ResultMapError()
    {
        var result = Result<string, string>.FromError("fail");
        return result.Map(v => 42);
    }

    [Benchmark]
    public int ResultMatchSuccess()
    {
        var result = Result<string, string>.FromValue("ok");
        return result.Match(v => 1, _ => 0);
    }

    [Benchmark]
    public int ResultMatchError()
    {
        var result = Result<string, string>.FromError("fail");
        return result.Match(v => 1, _ => 0);
    }

    [Benchmark]
    public ValueResult<string, string> ValueResultBindSuccess()
    {
        var result = ValueResult<string, string>.FromValue("ok");
        return result.Bind(v => ValueResult<string, string>.FromValue("bound"));
    }

    [Benchmark]
    public ValueResult<string, string> ValueResultBindError()
    {
        var result = ValueResult<string, string>.FromError("fail");
        return result.Bind(v => ValueResult<string, string>.FromValue("bound"));
    }

    [Benchmark]
    public ValueResult<int, string> ValueResultMapSuccess()
    {
        var result = ValueResult<string, string>.FromValue("ok");
        return result.Map(v => v.Length);
    }

    [Benchmark]
    public ValueResult<int, string> ValueResultMapError()
    {
        var result = ValueResult<string, string>.FromError("fail");
        return result.Map(v => v.Length);
    }

    [Benchmark]
    public int ValueResultMatchSuccess()
    {
        var result = ValueResult<string, string>.FromValue("ok");
        return result.Match(v => v.Length, _ => 0);
    }

    [Benchmark]
    public int ValueResultMatchError()
    {
        var result = ValueResult<string, string>.FromError("fail");
        return result.Match(v => v.Length, _ => 0);
    }

    #pragma warning restore CA1822 // Mark members as static
}
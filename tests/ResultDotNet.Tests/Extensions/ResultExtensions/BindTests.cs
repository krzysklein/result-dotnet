namespace ResultDotNet.Tests.Extensions.ResultExtensions;
using Result = ResultDotNet.Result;

public class BindTests
{
    [Fact]
    public void Success_Bind_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = result.Bind(() => Result.Success());

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public void Success_Bind_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = result.Bind(() => Result.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Error_Bind_Success_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = result.Bind(() => Result.Success());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Error_Bind_Error_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = result.Bind(() => Result.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Success_BindAsync_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Success()));

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public async Task Success_BindAsync_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Error()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Error_BindAsync_Success_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Success()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Error_BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Error()));

        // Assert
        Assert.True(bound.IsError);
    }
}
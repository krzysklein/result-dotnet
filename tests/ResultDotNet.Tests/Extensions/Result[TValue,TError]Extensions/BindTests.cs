namespace ResultDotNet.Tests.Extensions.ResultOfTValueTErrorExtensions;

public class BindTests
{
    [Fact]
    public void Bind_Success_InvokesFunc()
    {
        // Arrange
        var result = Result<string, string>.FromValue("ok");

        // Act
        var bound = result.Bind(v => Result<int, string>.FromValue(v.Length));

        // Assert
        Assert.True(bound.IsSuccess);
        Assert.Equal(2, bound.Value);
    }

    [Fact]
    public void Bind_Error_PropagatesError()
    {
        // Arrange
        var result = Result<string, string>.FromError("fail");

        // Act
        var bound = result.Bind(v => Result<int, string>.FromValue(v.Length));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public async Task BindAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = Result<string, string>.FromValue("ok");

        // Act
        var bound = await result.BindAsync(v => Task.FromResult(Result<int, string>.FromValue(v.Length)));

        // Assert
        Assert.True(bound.IsSuccess);
        Assert.Equal(2, bound.Value);
    }

    [Fact]
    public async Task BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = Result<string, string>.FromError("fail");

        // Act
        var bound = await result.BindAsync(v => Task.FromResult(Result<int, string>.FromValue(v.Length)));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }
}
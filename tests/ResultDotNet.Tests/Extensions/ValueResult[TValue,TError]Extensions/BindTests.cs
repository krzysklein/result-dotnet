namespace ResultDotNet.Tests.Extensions.ValueResultOfTValueTErrorExtensions;

public class BindTests
{
    [Fact]
    public void Bind_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var bound = result.Bind(v => ValueResult<int, string>.FromValue(v.Length));

        // Assert
        Assert.True(bound.IsSuccess);
        Assert.Equal(2, bound.Value);
    }

    [Fact]
    public void Bind_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var bound = result.Bind(v => ValueResult<int, string>.FromValue(v.Length));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public async Task BindAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var bound = await result.BindAsync(v => ValueTask.FromResult(ValueResult<int, string>.FromValue(v.Length)));

        // Assert
        Assert.True(bound.IsSuccess);
        Assert.Equal(2, bound.Value);
    }

    [Fact]
    public async Task BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var bound = await result.BindAsync(v => ValueTask.FromResult(ValueResult<int, string>.FromValue(v.Length)));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }
}
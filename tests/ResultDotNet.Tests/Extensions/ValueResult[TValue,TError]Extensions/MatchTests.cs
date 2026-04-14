namespace ResultDotNet.Tests.Extensions.ValueResultOfTValueTErrorExtensions;

public class MatchTests
{
    [Fact]
    public void Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = result.Match(v => v.Length, e => -1);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public void Match_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = result.Match(v => v.Length, e => -1);

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = await result.MatchAsync(v => v.Length, e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(v => v.Length, e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => -1);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => -1);

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(-1, value);
    }
}

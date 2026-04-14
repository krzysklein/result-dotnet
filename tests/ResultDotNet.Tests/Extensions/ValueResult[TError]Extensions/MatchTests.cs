namespace ResultDotNet.Tests.Extensions.ValueResultOfTErrorExtensions;

public class MatchTests
{
    [Fact]
    public void Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = result.Match(() => 1, e => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void Match_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = result.Match(() => 1, e => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = await result.MatchAsync(() => 1, e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => 1, e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

namespace ResultDotNet.Tests.Extensions.ValueResultExtensions;
using ValueResult = ResultDotNet.ValueResult;

public class MatchTests
{
    [Fact]
    public void Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void Match_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = await result.MatchAsync(() => 1, () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = await result.MatchAsync(() => 1, () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

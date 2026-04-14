namespace ResultDotNet.Tests.Extensions.ResultOfTErrorExtensions;

public class MatchTests
{
    [Fact]
    public void Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var value = result.Match(() => 1, e => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void Match_Error_InvokesOnError()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var value = result.Match(() => 1, e => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var value = await result.MatchAsync(() => 1, e => Task.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => 1, e => Task.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), e => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), e => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), e => Task.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), e => Task.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

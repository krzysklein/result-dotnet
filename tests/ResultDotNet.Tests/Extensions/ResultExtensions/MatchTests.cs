namespace ResultDotNet.Tests.Extensions.ResultExtensions;
using Result = ResultDotNet.Result;

public class MatchTests
{
    [Fact]
    public void Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result.Success();
        var counter = 0;

        // Act
        result.Match(
            () => { counter = 1; },
            () => { counter = 2; });

        // Assert
        Assert.Equal(1, counter);
    }

    [Fact]
    public void Match_Error_InvokesOnError()
    {
        // Arrange
        var result = Result.Error();
        var counter = 0;

        // Act
        result.Match(
            () => { counter = 1; },
            () => { counter = 2; });

        // Assert
        Assert.Equal(2, counter);
    }

    [Fact]
    public void MatchOfTSuccess_InvokesOnSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void MatchOfTError_InvokesOnError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result.Success();
        var counter = 0;

        // Act
        await result.MatchAsync(
            () => { counter = 1; },
            async () => { counter = 2; });

        // Assert
        Assert.Equal(1, counter);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = Result.Error();
        var counter = 0;

        // Act
        await result.MatchAsync(
            () => { counter = 1; },
            async () => { counter = 2; });

        // Assert
        Assert.Equal(2, counter);
    }

    [Fact]
    public async Task MatchOfTResultAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = await result.MatchAsync(() => 1, () => Task.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchOfTResultAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = await result.MatchAsync(() => 1, () => Task.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = Result.Success();
        var counter = 0;

        // Act
        await result.MatchAsync(
            async () => { counter = 1; },
            () => { counter = 2; });

        // Assert
        Assert.Equal(1, counter);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = Result.Error();
        var counter = 0;

        // Act
        await result.MatchAsync(
            async () => { counter = 1; },
            () => { counter = 2; });

        // Assert
        Assert.Equal(2, counter);
    }

    [Fact]
    public async Task MatchOfTResultAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchOfTResultAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = Result.Success();
        var counter = 0;

        // Act
        await result.MatchAsync(
            async () => { counter = 1; },
            async () => { counter = 2; });

        // Assert
        Assert.Equal(1, counter);
    }

    [Fact]
    public async Task MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = Result.Error();
        var counter = 0;

        // Act
        await result.MatchAsync(
            async () => { counter = 1; },
            async () => { counter = 2; });

        // Assert
        Assert.Equal(2, counter);
    }

    [Fact]
    public async Task MatchOfTResultAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => Task.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task MatchOfTResultAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => Task.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

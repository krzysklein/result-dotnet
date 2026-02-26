namespace ResultDotNet.Tests.Extensions;

public class ResultExtensionsTests
{
    [Fact]
    public void Result_Success_Bind_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = result.Bind(() => Result.Success());

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public void Result_Success_Bind_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = result.Bind(() => Result.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Result_Error_Bind_Success_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = result.Bind(() => Result.Success());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Result_Error_Bind_Error_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = result.Bind(() => Result.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Result_Success_BindAsync_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Success()));

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public async Task Result_Success_BindAsync_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Error()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Result_Error_BindAsync_Success_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Success()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Result_Error_BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result.Error()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Result_MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public void Result_MapError_Error_MapsError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public async Task Result_MapErrorAsync_Success_PropagatesSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var mapped = await result.MapErrorAsync(() => Task.FromResult(4));

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public async Task Result_MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var mapped = await result.MapErrorAsync(() => Task.FromResult(4));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public void Result_Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void Result_Match_Error_InvokesOnError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task Result_MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = await result.MatchAsync(() => 1, () => Task.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task Result_MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = await result.MatchAsync(() => 1, () => Task.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task Result_MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task Result_MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task Result_MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => Task.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task Result_MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var value = await result.MatchAsync(() => Task.FromResult(1), () => Task.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

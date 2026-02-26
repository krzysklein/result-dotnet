namespace ResultDotNet.Tests.Extensions;

public class ValueResultExtensionsTests
{
    [Fact]
    public void ValueResult_Success_Bind_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = result.Bind(() => ValueResult.Success());

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public void ValueResult_Success_Bind_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = result.Bind(() => ValueResult.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void ValueResult_Error_Bind_Success_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = result.Bind(() => ValueResult.Success());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void ValueResult_Error_Bind_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = result.Bind(() => ValueResult.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task ValueResult_Success_BindAsync_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Success()));

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public async Task ValueResult_Success_BindAsync_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Error()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task ValueResult_Error_BindAsync_Success_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Success()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task ValueResult_Error_BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Error()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void ValueResult_MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public void ValueResult_MapError_Error_MapsError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public async Task ValueResult_MapErrorAsync_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var mapped = await result.MapErrorAsync(() => ValueTask.FromResult(4));

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public async Task ValueResult_MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var mapped = await result.MapErrorAsync(() => ValueTask.FromResult(4));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public void ValueResult_Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void ValueResult_Match_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = result.Match(() => 1, () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResult_MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = await result.MatchAsync(() => 1, () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task ValueResult_MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = await result.MatchAsync(() => 1, () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResult_MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task ValueResult_MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResult_MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task ValueResult_MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), () => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

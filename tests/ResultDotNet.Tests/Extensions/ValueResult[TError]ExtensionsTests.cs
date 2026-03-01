namespace ResultDotNet.Tests.Extensions;

public class ValueResultOfTErrorExtensionsTests
{
    [Fact]
    public void ValueResultOfTError_Bind_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var bound = result.Bind(() => ValueResult<string>.FromError("fail"));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public void ValueResultOfTError_Bind_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var bound = result.Bind(() => ValueResult<string>.Success());

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public void ValueResultOfTError_BindTValue2_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var bound = result.Bind(() => ValueResult<int, string>.FromValue(42));

        // Assert
        Assert.True(bound.IsSuccess);
        Assert.Equal(42, bound.Value);
    }

    [Fact]
    public void ValueResultOfTError_BindTValue2_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var bound = result.Bind(() => ValueResult<int, string>.FromValue(42));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public async Task ValueResultOfTError_BindAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult<string>.FromError("fail")));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public async Task ValueResultOfTError_BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult<string>.Success()));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public void ValueResultOfTError_Map_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = result.Map(() => 123);

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(123, mapped.Value);
    }

    [Fact]
    public void ValueResultOfTError_Map_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = result.Map(() => 123);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }

    [Fact]
    public async Task ValueResultOfTError_MapAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = await result.MapAsync(() => ValueTask.FromResult(123));

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(123, mapped.Value);
    }

    [Fact]
    public async Task ValueResultOfTError_MapAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = await result.MapAsync(() => ValueTask.FromResult(123));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }

    [Fact]
    public void ValueResultOfTError_MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public void ValueResultOfTError_MapError_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public async Task ValueResultOfTError_MapErrorAsync_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public async Task ValueResultOfTError_MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public void ValueResultOfTError_Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = result.Match(() => 1, e => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public void ValueResultOfTError_Match_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = result.Match(() => 1, e => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResultOfTError_MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = await result.MatchAsync(() => 1, e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task ValueResultOfTError_MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => 1, e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResultOfTError_MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => 2);

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task ValueResultOfTError_MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => 2);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResultOfTError_MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(1, value);
    }

    [Fact]
    public async Task ValueResultOfTError_MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(() => ValueTask.FromResult(1), e => ValueTask.FromResult(2));

        // Assert
        Assert.Equal(2, value);
    }
}

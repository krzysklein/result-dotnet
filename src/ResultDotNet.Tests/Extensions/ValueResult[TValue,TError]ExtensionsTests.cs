namespace ResultDotNet.Tests.Extensions;

public class ValueResultOfTValueTErrorExtensionsTests
{
    [Fact]
    public void ValueResultOfTValueTError_Bind_Success_InvokesFunc()
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
    public void ValueResultOfTValueTError_Bind_Error_PropagatesError()
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
    public async Task ValueResultOfTValueTError_BindAsync_Success_InvokesFunc()
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
    public async Task ValueResultOfTValueTError_BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var bound = await result.BindAsync(v => ValueTask.FromResult(ValueResult<int, string>.FromValue(v.Length)));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public void ValueResultOfTValueTError_Map_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = result.Map(v => v.Length);

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(2, mapped.Value);
    }

    [Fact]
    public void ValueResultOfTValueTError_Map_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = result.Map(v => v.Length);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MapAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = await result.MapAsync(v => ValueTask.FromResult(v.Length));

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(2, mapped.Value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MapAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = await result.MapAsync(v => ValueTask.FromResult(v.Length));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }

    [Fact]
    public void ValueResultOfTValueTError_MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal("ok", mapped.Value);
    }

    [Fact]
    public void ValueResultOfTValueTError_MapError_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MapErrorAsync_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal("ok", mapped.Value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public void ValueResultOfTValueTError_Match_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = result.Match(v => v.Length, e => -1);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public void ValueResultOfTValueTError_Match_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = result.Match(v => v.Length, e => -1);

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MatchAsync_Success_InvokesOnSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = await result.MatchAsync(v => v.Length, e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MatchAsync_Error_InvokesOnErrorAsync()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(v => v.Length, e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MatchAsync_Success_InvokesOnSuccessAsync()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => -1);

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MatchAsync_Error_InvokesOnError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => -1);

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MatchAsync_Success_InvokesOnSuccessAsync2()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(2, value);
    }

    [Fact]
    public async Task ValueResultOfTValueTError_MatchAsync_Error_InvokesOnErrorAsync2()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var value = await result.MatchAsync(v => ValueTask.FromResult(v.Length), e => ValueTask.FromResult(-1));

        // Assert
        Assert.Equal(-1, value);
    }
}

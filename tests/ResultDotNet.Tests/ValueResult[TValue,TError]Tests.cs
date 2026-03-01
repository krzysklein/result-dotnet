namespace ResultDotNet.Tests;

public class ValueResultOfTValueTErrorTests
{
    [Fact]
    public void ValueResultOfTValueTError_default_CreatesSuccessResult()
    {
        // Act
        ValueResult<string, string> result = default;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Null(result.Value);
    }

    [Fact]
    public void ValueResultOfTValueTError_Success_CreatesSuccessResult()
    {
        // Act
        var result = ValueResult<string, string>.FromValue("ok");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Equal("ok", result.Value);
    }

    [Fact]
    public void ValueResultOfTValueTError_Error_CreatesErrorResult()
    {
        // Act
        var result = ValueResult<string, string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }

    [Fact]
    public void ValueResultOfTValueTError_Value_ThrowsOnError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }

    [Fact]
    public void ValueResultOfTValueTError_Error_ThrowsOnSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }

    [Fact]
    public void ValueResultOfTValueTError_ImplicitConversion_Success_CreatesSuccessResult()
    {
        // Arrange
        ValueResult<string, int> result = "ok";

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Equal("ok", result.Value);
    }

    [Fact]
    public void ValueResultOfTValueTError_ImplicitConversion_Error_CreatesErrorResult()
    {
        // Arrange
        ValueResult<string, int> result = 5;

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal(5, result.Error);
    }
}

namespace ResultDotNet.Tests;

public class ValueResultOfTErrorTests
{
    [Fact]
    public void ValueResultOfTError_default_CreatesSuccessResult()
    {
        // Act
        ValueResult<string> result = default;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void ValueResultOfTError_Success_CreatesSuccessResult()
    {
        // Act
        var result = ValueResult<string>.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void ValueResultOfTError_Error_CreatesErrorResult()
    {
        // Act
        var result = ValueResult<string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }

    [Fact]
    public void ValueResultOfTError_Error_ThrowsOnSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }

    [Fact]
    public void ValueResultOfTError_ImplicitConversion_Error_CreatesErrorResult()
    {
        // Arrange
        ValueResult<string> result = "fail";

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }
}

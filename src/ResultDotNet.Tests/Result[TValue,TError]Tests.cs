namespace ResultDotNet.Tests;

public class ResultOfTValueTErrorTests
{
    [Fact]
    public void ResultOfTValueTError_Success_CreatesSuccessResult()
    {
        // Act
        var result = Result<string, string>.FromValue("ok");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Equal("ok", result.Value);
    }

    [Fact]
    public void ResultOfTValueTError_Error_CreatesErrorResult()
    {
        // Act
        var result = Result<string, string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }

    [Fact]
    public void ResultOfTValueTError_Value_ThrowsOnError()
    {
        // Arrange
        var result = Result<string, string>.FromError("fail");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }

    [Fact]
    public void ResultOfTValueTError_Error_ThrowsOnSuccess()
    {
        // Arrange
        var result = Result<string, string>.FromValue("ok");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }

    [Fact]
    public void ResultOfTError_Success_CreatesSuccessResult()
    {
        // Act
        var result = Result<string>.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void ResultOfTError_Error_CreatesErrorResult()
    {
        // Act
        var result = Result<string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }

    [Fact]
    public void ResultOfTError_Error_ThrowsOnSuccess()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }
}

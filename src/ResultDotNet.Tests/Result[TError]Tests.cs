namespace ResultDotNet.Tests;

public class ResultOfTErrorTests
{
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

    [Fact]
    public void ResultOfTError_ImplicitConversion_Error_CreatesErrorResult()
    {
        // Arrange
        Result<string> result = "fail";

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }
}

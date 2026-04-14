namespace ResultDotNet.Tests.ResultOfTValueTError;

public class ImplicitConversionTests
{
    [Fact]
    public void ImplicitConversion_TValue_CreatesSuccessResult()
    {
        // Arrange
        Result<string, int> result = "ok";

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Equal("ok", result.Value);
    }

    [Fact]
    public void ImplicitConversion_TError_CreatesErrorResult()
    {
        // Arrange
        Result<string, int> result = 5;

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal(5, result.Error);
    }
}

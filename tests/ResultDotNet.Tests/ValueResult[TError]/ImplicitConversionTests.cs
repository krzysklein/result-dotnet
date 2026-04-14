namespace ResultDotNet.Tests.ValueResultOfTError;

public class ImplicitConversionTests
{
    [Fact]
    public void ImplicitConversion_Default_CreatesSuccessResult()
    {
        // Act
        ValueResult<string> result = default;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void ImplicitConversion_TError_CreatesErrorResult()
    {
        // Arrange
        ValueResult<string> result = "fail";

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }
}

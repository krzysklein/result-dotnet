namespace ResultDotNet.Tests.ResultOfTError;

public class ImplicitConversionTests
{
    [Fact]
    public void ImplicitConversion_TError_CreatesErrorResult()
    {
        // Arrange & Act
        Result<string> result = "fail";

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }
}

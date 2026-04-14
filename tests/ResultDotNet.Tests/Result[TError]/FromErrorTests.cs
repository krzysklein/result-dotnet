namespace ResultDotNet.Tests.ResultOfTError;

public class FromErrorTests
{
    [Fact]
    public void FromError_CreatesErrorResult()
    {
        // Arrange & Act
        var result = Result<string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }
}
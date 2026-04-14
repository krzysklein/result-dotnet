namespace ResultDotNet.Tests.ValueResultOfTError;

public class FromErrorTests
{
    [Fact]
    public void FromError_CreatesErrorResult()
    {
        // Act
        var result = ValueResult<string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }
}
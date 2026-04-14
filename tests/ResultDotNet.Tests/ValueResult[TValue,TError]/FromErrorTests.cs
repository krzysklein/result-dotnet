namespace ResultDotNet.Tests.ValueResultOfTValueTError;

public class FromErrorTests
{
    [Fact]
    public void FromError_CreatesErrorResult()
    {
        // Act
        var result = ValueResult<string, string>.FromError("fail");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Equal("fail", result.Error);
    }

    [Fact]
    public void FromError_GetValue_ThrowsOnError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }
}

namespace ResultDotNet.Tests.ResultOfTValueTError;

public class FromValueTests
{
    [Fact]
    public void FromValue_CreatesSuccessResult()
    {
        // Act
        var result = Result<string, string>.FromValue("ok");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Equal("ok", result.Value);
    }

    [Fact]
    public void FromValue_GetError_ThrowsOnSuccess()
    {
        // Arrange
        var result = Result<string, string>.FromValue("ok");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }
}
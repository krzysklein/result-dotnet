namespace ResultDotNet.Tests.ValueResultOfTError;

public class SuccessTests
{
    [Fact]
    public void Success_CreatesSuccessResult()
    {
        // Act
        var result = ValueResult<string>.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void Success_GetError_ThrowsInvalidOperationException()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }
}
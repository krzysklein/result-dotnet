namespace ResultDotNet.Tests.ResultOfTError;

public class SuccessTests
{
    [Fact]
    public void Success_CreatesSuccessResult()
    {
        // Act
        var result = Result<string>.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void Success_ThrowsOnError()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }
}
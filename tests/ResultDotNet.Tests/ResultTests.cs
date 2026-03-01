namespace ResultDotNet.Tests;

public class ResultTests
{
    [Fact]
    public void Result_Success_CreatesSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void Result_Error_CreatesErrorResult()
    {
        // Act
        var result = Result.Error();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
    }
}

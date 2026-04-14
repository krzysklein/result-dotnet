namespace ResultDotNet.Tests.Result;
using Result = ResultDotNet.Result;

public class SuccessTests
{
    [Fact]
    public void Success_CreatesSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }
}
namespace ResultDotNet.Tests.Result;
using Result = ResultDotNet.Result;

public class ErrorTests
{
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

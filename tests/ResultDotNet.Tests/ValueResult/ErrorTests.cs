namespace ResultDotNet.Tests.ValueResult;
using ValueResult = ResultDotNet.ValueResult;

public class ErrorTests
{
    [Fact]
    public void ValueResult_Error_CreatesErrorResult()
    {
        // Act
        var result = ValueResult.Error();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
    }
}

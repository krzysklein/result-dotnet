namespace ResultDotNet.Tests.ValueResult;
using ValueResult = ResultDotNet.ValueResult;

public class SuccessTests
{
    [Fact]
    public void ValueResult_Default_CreatesSuccessResult()
    {
        // Act
        ValueResult result = default;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void ValueResult_Success_CreatesSuccessResult()
    {
        // Act
        var result = ValueResult.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }
}
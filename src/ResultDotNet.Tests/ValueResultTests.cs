namespace ResultDotNet.Tests;

public class ValueResultTests
{
    [Fact]
    public void ValueResult_default_CreatesSuccessResult()
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

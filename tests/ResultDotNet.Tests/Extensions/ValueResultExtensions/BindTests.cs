namespace ResultDotNet.Tests.Extensions.ValueResultExtensions;
using ValueResult = ResultDotNet.ValueResult;

public class BindTests
{
    [Fact]
    public void Success_Bind_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = result.Bind(() => ValueResult.Success());

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public void Success_Bind_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = result.Bind(() => ValueResult.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Error_Bind_Success_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = result.Bind(() => ValueResult.Success());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public void Error_Bind_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = result.Bind(() => ValueResult.Error());

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Success_BindAsync_Success_InvokesFuncAndReturnsSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Success()));

        // Assert
        Assert.True(bound.IsSuccess);
    }

    [Fact]
    public async Task Success_BindAsync_Error_InvokesFuncAndReturnsError()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Error()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Error_BindAsync_Success_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Success()));

        // Assert
        Assert.True(bound.IsError);
    }

    [Fact]
    public async Task Error_BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var bound = await result.BindAsync(() => ValueTask.FromResult(ValueResult.Error()));

        // Assert
        Assert.True(bound.IsError);
    }
}
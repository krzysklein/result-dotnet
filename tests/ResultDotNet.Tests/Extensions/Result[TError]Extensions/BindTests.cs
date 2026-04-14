namespace ResultDotNet.Tests.Extensions.ResultOfTErrorExtensions;

public class BindTests
{
    [Fact]
    public void Bind_Success_InvokesFunc()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var bound = result.Bind(() => Result<string>.FromError("fail"));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public void Bind_Error_PropagatesError()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var bound = result.Bind(() => Result<string>.Success());

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public void BindTValue2_Success_InvokesFunc()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var bound = result.Bind(() => Result<int, string>.FromValue(42));

        // Assert
        Assert.True(bound.IsSuccess);
        Assert.Equal(42, bound.Value);
    }

    [Fact]
    public void BindTValue2_Error_PropagatesError()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var bound = result.Bind(() => Result<int, string>.FromValue(42));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public async Task BindAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = Result<string>.Success();

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result<string>.FromError("fail")));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }

    [Fact]
    public async Task BindAsync_Error_PropagatesError()
    {
        // Arrange
        var result = Result<string>.FromError("fail");

        // Act
        var bound = await result.BindAsync(() => Task.FromResult(Result<string>.Success()));

        // Assert
        Assert.True(bound.IsError);
        Assert.Equal("fail", bound.Error);
    }
}
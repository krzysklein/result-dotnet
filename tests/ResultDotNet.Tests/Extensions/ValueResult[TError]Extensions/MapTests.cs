namespace ResultDotNet.Tests.Extensions.ValueResultOfTErrorExtensions;

public class MapTests
{
    [Fact]
    public void Map_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = result.Map(() => 123);

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(123, mapped.Value);
    }

    [Fact]
    public void Map_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = result.Map(() => 123);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }

    [Fact]
    public async Task MapAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = await result.MapAsync(() => ValueTask.FromResult(123));

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(123, mapped.Value);
    }

    [Fact]
    public async Task MapAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = await result.MapAsync(() => ValueTask.FromResult(123));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }
}

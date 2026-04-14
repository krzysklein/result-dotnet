namespace ResultDotNet.Tests.Extensions.ValueResultOfTValueTErrorExtensions;

public class MapTests
{
    [Fact]
    public void Map_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = result.Map(v => v.Length);

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(2, mapped.Value);
    }

    [Fact]
    public void Map_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = result.Map(v => v.Length);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }

    [Fact]
    public async Task MapAsync_Success_InvokesFunc()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = await result.MapAsync(v => ValueTask.FromResult(v.Length));

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal(2, mapped.Value);
    }

    [Fact]
    public async Task MapAsync_Error_PropagatesError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = await result.MapAsync(v => ValueTask.FromResult(v.Length));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal("fail", mapped.Error);
    }
}

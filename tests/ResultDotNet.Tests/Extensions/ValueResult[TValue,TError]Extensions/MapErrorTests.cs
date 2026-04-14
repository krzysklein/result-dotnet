namespace ResultDotNet.Tests.Extensions.ValueResultOfTValueTErrorExtensions;

public class MapErrorTests
{
    [Fact]
    public void MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal("ok", mapped.Value);
    }

    [Fact]
    public void MapError_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public async Task MapErrorAsync_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string, string>.FromValue("ok");

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsSuccess);
        Assert.Equal("ok", mapped.Value);
    }

    [Fact]
    public async Task MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string, string>.FromError("fail");

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }
}

namespace ResultDotNet.Tests.Extensions.ValueResultOfTErrorExtensions;

public class MapErrorTests
{
    [Fact]
    public void MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult<string>.Success();

        // Act
        var mapped = result.MapError(e => e.Length);

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public void MapError_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

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
        var result = ValueResult<string>.Success();

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public async Task MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = ValueResult<string>.FromError("fail");

        // Act
        var mapped = await result.MapErrorAsync(e => ValueTask.FromResult(e.Length));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }
}

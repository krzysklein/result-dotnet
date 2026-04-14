namespace ResultDotNet.Tests.Extensions.ValueResultExtensions;
using ValueResult = ResultDotNet.ValueResult;

public class MapErrorTests
{
    [Fact]
    public void MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public void MapError_Error_MapsError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }

    [Fact]
    public async Task MapErrorAsync_Success_PropagatesSuccess()
    {
        // Arrange
        var result = ValueResult.Success();

        // Act
        var mapped = await result.MapErrorAsync(() => ValueTask.FromResult(4));

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public async Task MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = ValueResult.Error();

        // Act
        var mapped = await result.MapErrorAsync(() => ValueTask.FromResult(4));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }
}

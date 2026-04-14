namespace ResultDotNet.Tests.Extensions.ResultExtensions;
using Result = ResultDotNet.Result;

public class MapErrorTests
{
    [Fact]
    public void MapError_Success_PropagatesSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var mapped = result.MapError(() => 4);

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public void MapError_Error_MapsError()
    {
        // Arrange
        var result = Result.Error();

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
        var result = Result.Success();

        // Act
        var mapped = await result.MapErrorAsync(() => Task.FromResult(4));

        // Assert
        Assert.True(mapped.IsSuccess);
    }

    [Fact]
    public async Task MapErrorAsync_Error_MapsError()
    {
        // Arrange
        var result = Result.Error();

        // Act
        var mapped = await result.MapErrorAsync(() => Task.FromResult(4));

        // Assert
        Assert.True(mapped.IsError);
        Assert.Equal(4, mapped.Error);
    }
}

namespace ResultDotNet.Tests.Extensions.ReferenceTypeExtensions;

public class ConvertToResultTests
{
    [Fact]
    public void ConvertToResult_NotNull_ReturnsSuccess()
    {
        // Arrange
        string? str = "Hello";

        // Act
        var result = str.ConvertToResult();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Hello", result.Value);
    }

    [Fact]
    public void ConvertToResult_Null_ReturnsError()
    {
        // Arrange
        string? str = null;

        // Act
        var result = str.ConvertToResult();

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Reference is null.", result.Error);
    }

    [Fact]
    public void ConvertToResult_NotNull_WithErrorFactory_ReturnsSuccess()
    {
        // Arrange
        string? str = "Hello";

        // Act
        var result = str.ConvertToResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Hello", result.Value);
    }

    [Fact]
    public void ConvertToResult_WithErrorFactory_Null_ReturnsCustomError()
    {
        // Arrange
        string? str = null;

        // Act
        var result = str.ConvertToResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Custom error message", result.Error);
    }
}
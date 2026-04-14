namespace ResultDotNet.Tests.Extensions.ReferenceTypeExtensions;

public class ConvertToValueResultTests
{
    [Fact]
    public void ConvertToValueResult_NotNull_ReturnsSuccess()
    {
        // Arrange
        string? str = "Hello";

        // Act
        var result = str.ConvertToValueResult();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Hello", result.Value);
    }

    [Fact]
    public void ConvertToValueResult_Null_ReturnsError()
    {
        // Arrange
        string? str = null;

        // Act
        var result = str.ConvertToValueResult();

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Reference is null.", result.Error);
    }

    [Fact]
    public void ConvertToValueResult_NotNull_WithErrorFactory_ReturnsSuccess()
    {
        // Arrange
        string? str = "Hello";

        // Act
        var result = str.ConvertToValueResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Hello", result.Value);
    }

    [Fact]
    public void ConvertToValueResult_WithErrorFactory_Null_ReturnsCustomError()
    {
        // Arrange
        string? str = null;

        // Act
        var result = str.ConvertToValueResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Custom error message", result.Error);
    }
}
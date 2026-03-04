namespace ResultDotNet.Tests.Extensions;

public class NullableExtensionsTests
{
    [Fact]
    public void ConvertToResult_HasValue_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;

        // Act
        var result = nullable.ConvertToResult();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ConvertToResult_NoValue_ReturnsError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ConvertToResult();

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Value is null.", result.Error);
    }

    [Fact]
    public void ConvertToResult_HasValue_WithErrorFactory_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;

        // Act
        var result = nullable.ConvertToResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ConvertToResult_WithErrorFactory_NoValue_ReturnsCustomError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ConvertToResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Custom error message", result.Error);
    }

    [Fact]
    public void ConvertToValueResult_HasValue_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;

        // Act
        var result = nullable.ConvertToValueResult();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ConvertToValueResult_NoValue_ReturnsError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ConvertToValueResult();

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Value is null.", result.Error);
    }

    [Fact]
    public void ConvertToValueResult_HasValue_WithErrorFactory_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;
        // Act
        var result = nullable.ConvertToValueResult(() => "Custom error message");
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ConvertToValueResult_WithErrorFactory_NoValue_ReturnsCustomError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ConvertToValueResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Custom error message", result.Error);
    }
}
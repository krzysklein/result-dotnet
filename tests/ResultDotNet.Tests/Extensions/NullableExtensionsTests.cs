namespace ResultDotNet.Tests.Extensions;

public class NullableExtensionsTests
{
    [Fact]
    public void ToResult_HasValue_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;

        // Act
        var result = nullable.ToResult();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ToResult_NoValue_ReturnsError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ToResult();

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Value is null.", result.Error);
    }

    [Fact]
    public void ToResult_HasValue_WithErrorFactory_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;

        // Act
        var result = nullable.ToResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ToResult_WithErrorFactory_NoValue_ReturnsCustomError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ToResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Custom error message", result.Error);
    }

    [Fact]
    public void ToValueResult_HasValue_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;

        // Act
        var result = nullable.ToValueResult();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ToValueResult_NoValue_ReturnsError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ToValueResult();

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Value is null.", result.Error);
    }

    [Fact]
    public void ToValueResult_HasValue_WithErrorFactory_ReturnsSuccess()
    {
        // Arrange
        int? nullable = 42;
        // Act
        var result = nullable.ToValueResult(() => "Custom error message");
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ToValueResult_WithErrorFactory_NoValue_ReturnsCustomError()
    {
        // Arrange
        int? nullable = null;

        // Act
        var result = nullable.ToValueResult(() => "Custom error message");

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Custom error message", result.Error);
    }
}
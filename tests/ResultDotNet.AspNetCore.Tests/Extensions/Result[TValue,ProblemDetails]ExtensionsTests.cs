using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.AspNetCore.Tests.Extensions;

public class ResultOfTValueProblemDetailsExtensionsTests
{
    [Fact]
    public void ResultOfTValueProblemDetails_ToActionResult_Success_ReturnsObjectResultWith200HttpStatusCode()
    {
        // Arrange
        Result<string, ProblemDetails> result = "ok";

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var okResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal("ok", okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void ResultOfTValueProblemDetails_ToActionResultWithCustomHttpStatusCode_Success_ReturnsObjectResultWithSpecifiedHttpStatusCode()
    {
        // Arrange
        Result<string, ProblemDetails> result = "ok";

        // Act
        var actionResult = result.ToActionResult(System.Net.HttpStatusCode.Accepted);

        // Assert
        var okResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal("ok", okResult.Value);
        Assert.Equal(202, okResult.StatusCode);
    }

    [Fact]
    public void ResultOfTValueProblemDetails_ToActionResult_Error_ReturnsObjectResultWithProblemDetails()
    {
        // Arrange
        var problemDetails = new ProblemDetails { Title = "Error", Status = 400 };
        Result<string, ProblemDetails> result = problemDetails;

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(problemDetails, objectResult.Value);
        Assert.Equal(400, objectResult.StatusCode);
    }
}

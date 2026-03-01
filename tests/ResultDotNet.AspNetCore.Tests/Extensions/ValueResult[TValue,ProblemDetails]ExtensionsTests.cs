using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.AspNetCore.Tests.Extensions;

public class ValueResultOfTValueProblemDetailsExtensionsTests
{
    [Fact]
    public void ValueResultOfTValueProblemDetails_ToActionResult_Success_ReturnsObjectResultWith200HttpStatusCode()
    {
        // Arrange
        ValueResult<string, ProblemDetails> result = "ok";

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var okResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal("ok", okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void ValueResultOfTValueProblemDetails_ToActionResultWithCustomHttpStatusCode_Success_ReturnsObjectResultWithSpecifiedHttpStatusCode()
    {
        // Arrange
        ValueResult<string, ProblemDetails> result = "ok";

        // Act
        var actionResult = result.ToActionResult(System.Net.HttpStatusCode.Accepted);

        // Assert
        var okResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal("ok", okResult.Value);
        Assert.Equal(202, okResult.StatusCode);
    }

    [Fact]
    public void ValueResultOfTValueProblemDetails_ToActionResult_Error_ReturnsObjectResultWithProblemDetails()
    {
        // Arrange
        var problemDetails = new ProblemDetails { Title = "Error", Status = 400 };
        ValueResult<string, ProblemDetails> result = problemDetails;

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(problemDetails, objectResult.Value);
        Assert.Equal(400, objectResult.StatusCode);
    }
}

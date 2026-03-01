using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.AspNetCore.Tests.Extensions;

public class ResultOfProblemDetailsExtensionsTests
{
    [Fact]
    public void ResultOfProblemDetails_ToActionResult_Success_ReturnsStatusCodeResultWith204HttpStatusCode()
    {
        // Arrange
        Result<ProblemDetails> result = Result<ProblemDetails>.Success();

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }

    [Fact]
    public void ResultOfProblemDetails_ToActionResultWithCustomHttpStatusCode_Success_ReturnsStatusCodeResultWithSpecifiedHttpStatusCode()
    {
        // Arrange
        Result<ProblemDetails> result = Result<ProblemDetails>.Success();

        // Act
        var actionResult = result.ToActionResult(System.Net.HttpStatusCode.Accepted);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
        Assert.Equal(202, statusCodeResult.StatusCode);
    }

    [Fact]
    public void ResultOfProblemDetails_ToActionResult_Error_ReturnsObjectResultWithProblemDetails()
    {
        // Arrange
        var problemDetails = new ProblemDetails { Title = "Error", Status = 400 };
        Result<ProblemDetails> result = problemDetails;

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(problemDetails, objectResult.Value);
        Assert.Equal(400, objectResult.StatusCode);
    }
}

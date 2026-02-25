using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.AspNetCore.Tests.Extensions;

public class ValueResultOfProblemDetailsExtensionsTests
{
    [Fact]
    public void ValueResultOfProblemDetails_ToActionResult_Success_ReturnsStatusCodeResultWith204HttpStatusCode()
    {
        // Arrange
        ValueResult<ProblemDetails> result = ValueResult<ProblemDetails>.Success();

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }

    [Fact]
    public void ValueResultOfProblemDetails_ToActionResultWithCustomHttpStatusCode_Success_ReturnsStatusCodeResultWithSpecifiedHttpStatusCode()
    {
        // Arrange
        ValueResult<ProblemDetails> result = ValueResult<ProblemDetails>.Success();

        // Act
        var actionResult = result.ToActionResult(System.Net.HttpStatusCode.Accepted);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);
        Assert.Equal(202, statusCodeResult.StatusCode);
    }

    [Fact]
    public void ValueResultOfProblemDetails_ToActionResult_Error_ReturnsObjectResultWithProblemDetails()
    {
        // Arrange
        var problemDetails = new ProblemDetails { Title = "Error", Status = 400 };
        ValueResult<ProblemDetails> result = problemDetails;

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(problemDetails, objectResult.Value);
        Assert.Equal(400, objectResult.StatusCode);
    }
}

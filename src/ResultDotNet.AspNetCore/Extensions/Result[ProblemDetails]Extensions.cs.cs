using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ResultOfProblemDetailsExtensions
{
    extension(Result<ProblemDetails> result)
    {
        /// <summary>
        /// Converts the current result to an ASP.NET Core IActionResult, using the specified HTTP status code for
        /// successful outcomes.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code to use if the result represents a success. The default is 204 (No Content).</param>
        /// <returns>An IActionResult representing the result. Returns a StatusCodeResult with the specified status code if
        /// successful; otherwise, returns an ObjectResult containing the error details.</returns>
        public IActionResult ToActionResult(HttpStatusCode httpStatusCode = HttpStatusCode.NoContent) 
            => result.Match<ProblemDetails, IActionResult>(
                onSuccess: () => new StatusCodeResult((int)httpStatusCode),
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ResultOfProblemDetailsExtensions
{
    extension(Result<ProblemDetails> result)
    {
        public IActionResult ToActionResult(HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            return result.Match<ProblemDetails, IActionResult>(
                onSuccess: () => new StatusCodeResult((int)httpStatusCode),
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
        }
    }
}
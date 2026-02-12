using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ResultOfTValueProblemDetailsExtensions
{
    extension<TValue>(Result<TValue, ProblemDetails> result)
    {
        public IActionResult ToActionResult(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return result.Match<TValue, ProblemDetails, IActionResult>(
                onSuccess: value => new ObjectResult(value) { StatusCode = (int)httpStatusCode },
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;

public static class TaskOfResultOfTErrorExtensions
{
    extension(Task<Result<ProblemDetails>> result)
    {
        public async Task<IActionResult> ToActionResultAsync(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return (await result).Match<ProblemDetails, IActionResult>(
                onSuccess: () => new StatusCodeResult((int)httpStatusCode),
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
        }
    }
}
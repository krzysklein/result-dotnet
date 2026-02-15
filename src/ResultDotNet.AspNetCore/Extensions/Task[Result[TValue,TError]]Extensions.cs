using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;

public static class TaskOfResultOfTValueTErrorExtensions
{
    extension<TValue>(Task<Result<TValue, ProblemDetails>> result)
    {
        public async Task<IActionResult> ToActionResultAsync(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return (await result).Match<TValue, ProblemDetails, IActionResult>(
                onSuccess: value => new ObjectResult(value) { StatusCode = (int)httpStatusCode },
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
        }
    }
}
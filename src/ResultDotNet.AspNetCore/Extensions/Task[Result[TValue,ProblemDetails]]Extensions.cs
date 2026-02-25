using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;

public static class TaskOfResultOfTValueProblemDetailsExtensions
{
    extension<TValue>(Task<Result<TValue, ProblemDetails>> result)
    {
        /// <summary>
        /// Asynchronously converts the result to an <see cref="IActionResult"/> with the specified HTTP status code.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code to use for the response. The default is <see cref="HttpStatusCode.OK"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/>
        /// representing either a successful value or a problem details response.</returns>
        public async Task<IActionResult> ToActionResultAsync(HttpStatusCode httpStatusCode = HttpStatusCode.OK) 
            => (await result).Match<TValue, ProblemDetails, IActionResult>(
                onSuccess: value => new ObjectResult(value) { StatusCode = (int)httpStatusCode },
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
    }
}
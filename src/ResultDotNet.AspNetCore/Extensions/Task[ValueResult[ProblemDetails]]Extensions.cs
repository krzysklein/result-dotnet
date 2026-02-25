using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;

public static class TaskOfValueResultOfProblemDetailsExtensions
{
    extension(Task<ValueResult<ProblemDetails>> result)
    {
        /// <summary>
        /// Asynchronously converts the result to an ASP.NET Core IActionResult, using the specified HTTP status code
        /// for successful outcomes or returning a ProblemDetails response for errors.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code to use for successful results. The default is HttpStatusCode.OK (200).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult representing
        /// either a success with the specified status code or an error with ProblemDetails.</returns>
        public async Task<IActionResult> ToActionResultAsync(HttpStatusCode httpStatusCode = HttpStatusCode.OK) 
            => (await result).Match<ProblemDetails, IActionResult>(
                onSuccess: () => new StatusCodeResult((int)httpStatusCode),
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
    }
}
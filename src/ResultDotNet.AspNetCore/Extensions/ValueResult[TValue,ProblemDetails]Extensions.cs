using Microsoft.AspNetCore.Mvc;
using System.Net;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ValueResultOfTValueProblemDetailsExtensions
{
    extension<TValue>(ValueResult<TValue, ProblemDetails> result)
    {
        /// <summary>
        /// Converts the current result to an <see cref="IActionResult"/> suitable for use in ASP.NET Core controllers.
        /// </summary>
        /// <remarks>If the result is successful, the returned <see cref="ObjectResult"/> will have its
        /// <c>StatusCode</c> set to the specified <paramref name="httpStatusCode"/>. If the result is an error, the
        /// <see cref="ProblemDetails.Status"/> value is used as the status code.</remarks>
        /// <param name="httpStatusCode">The HTTP status code to use if the result represents a successful value. The default is <see
        /// cref="HttpStatusCode.OK"/>.</param>
        /// <returns>An <see cref="IActionResult"/> representing either the successful value or a problem details response,
        /// depending on the result state.</returns>
        public IActionResult ToActionResult(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
            => result.Match<TValue, ProblemDetails, IActionResult>(
                onSuccess: value => new ObjectResult(value) { StatusCode = (int)httpStatusCode },
                onError: error => new ObjectResult(error) { StatusCode = error.Status }
            );
    }
}
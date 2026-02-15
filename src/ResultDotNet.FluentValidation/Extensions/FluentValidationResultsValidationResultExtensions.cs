using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class FluentValidationResultsValidationResultExtensions
{
    extension(ValidationResult validationResult)
    {
        /// <summary>
        /// Converts the current validation result to a <see cref="Result{T}"/> containing <see cref="ProblemDetails"/>
        /// if validation failed, or a successful result if validation passed.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to assign to the <see cref="ProblemDetails"/> if validation failed. Defaults to 400
        /// (Bad Request).</param>
        /// <param name="title">The title to assign to the <see cref="ProblemDetails"/> if validation failed. Defaults to "Validation
        /// failed.".</param>
        /// <param name="errorSeparator">The string used to separate individual error messages in the <see cref="ProblemDetails.Detail"/> property.
        /// Defaults to "; ".</param>
        /// <returns>A successful <see cref="Result{T}"/> if validation passed; otherwise, a failed result containing a <see
        /// cref="ProblemDetails"/> instance with details about the validation errors.</returns>
        public Result<ProblemDetails> ToResult(
            int statusCode = StatusCodes.Status400BadRequest,
            string title = "Validation failed.",
            string errorSeparator = "; ") 
            => validationResult.IsValid
                ? Result<ProblemDetails>.Success()
                : Result<ProblemDetails>.FromError(new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = string.Join(errorSeparator, validationResult.Errors.Select(e => e.ErrorMessage))
                });
    }
}
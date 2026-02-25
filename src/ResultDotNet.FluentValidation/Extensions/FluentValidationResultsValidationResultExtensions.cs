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

        /// <summary>
        /// Converts the validation result to a ValueResult containing a ProblemDetails object if validation fails, or a
        /// successful result if validation passes.
        /// </summary>
        /// <remarks>If validation fails, the returned ProblemDetails object includes all error messages
        /// concatenated using the specified separator. The method is useful for converting validation outcomes into
        /// standardized error responses suitable for HTTP APIs.</remarks>
        /// <param name="statusCode">The HTTP status code to assign to the ProblemDetails object when validation fails. Defaults to 400 (Bad
        /// Request).</param>
        /// <param name="title">The title to use for the ProblemDetails object when validation fails. Defaults to "Validation failed.".</param>
        /// <param name="errorSeparator">The separator string used to join multiple validation error messages in the ProblemDetails detail field.
        /// Defaults to "; ".</param>
        /// <returns>A ValueResult containing a ProblemDetails object with validation error details if validation fails;
        /// otherwise, a successful ValueResult with no error.</returns>
        public ValueResult<ProblemDetails> ToValueResult(
            int statusCode = StatusCodes.Status400BadRequest,
            string title = "Validation failed.",
            string errorSeparator = "; ")
            => validationResult.IsValid
                ? ValueResult<ProblemDetails>.Success()
                : ValueResult<ProblemDetails>.FromError(new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = string.Join(errorSeparator, validationResult.Errors.Select(e => e.ErrorMessage))
                });
    }
}
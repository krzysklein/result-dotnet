#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;

public static class NullableExtensions
{
    private const string ValueIsNullErrorMessage = "Value is null.";

    extension<TValue>(TValue? nullable)
        where TValue : struct
    {
        /// <summary>
        /// Converts a nullable value type to a Result, returning a successful result if the nullable has a value,
        /// </summary>
        /// <remarks>If the nullable does not have a value, a failed result is returned with a default error message. This method is useful for
        /// integrating nullable types into result-based workflows, allowing for seamless error handling when dealing with potentially null values.</remarks>
        /// <returns>A Result containing the value of the nullable if it has one, or an error message if it does not.</returns>
        public Result<TValue, string> ConvertToResult()
            => nullable.HasValue
                ? Result<TValue, string>.FromValue(nullable.Value)
                : Result<TValue, string>.FromError(ValueIsNullErrorMessage);

        /// <summary>
        /// Converts the current nullable value to a result, returning a success if a value is present or an error if
        /// not.
        /// </summary>
        /// <remarks>Use this method to handle nullable values in a functional style, enabling explicit
        /// error handling when a value is missing.</remarks>
        /// <typeparam name="TError">The type of the error to return if the nullable value is not present.</typeparam>
        /// <param name="errorFactory">A function that produces an error of type TError when the nullable value is absent.</param>
        /// <returns>A Result containing the value if present; otherwise, a Result containing the error produced by errorFactory.</returns>
        public Result<TValue, TError> ConvertToResult<TError>(Func<TError> errorFactory)
            => nullable.HasValue
                ? Result<TValue, TError>.FromValue(nullable.Value)
                : Result<TValue, TError>.FromError(errorFactory());

        /// <summary>
        /// Converts the current nullable value to a ValueResult, returning the value if present or an error message if
        /// the value is null.
        /// </summary>
        /// <remarks>This method provides a consistent way to handle nullable types by encapsulating the
        /// result in a ValueResult, which simplifies error handling and result processing in calling code.</remarks>
        /// <returns>A ValueResult containing the value of the nullable if it has a value; otherwise, a ValueResult containing an
        /// error message indicating that the value is null.</returns>
        public ValueResult<TValue, string> ConvertToValueResult()
            => nullable.HasValue
                ? ValueResult<TValue, string>.FromValue(nullable.Value)
                : ValueResult<TValue, string>.FromError(ValueIsNullErrorMessage);

        /// <summary>
        /// Converts the current nullable value to a ValueResult, returning either the contained value if present or an
        /// error if the value is absent.
        /// </summary>
        /// <remarks>This method provides a convenient way to handle nullable values by encapsulating both
        /// success and error states in a ValueResult. It is useful for scenarios where you want to propagate errors in
        /// a functional style rather than using exceptions.</remarks>
        /// <typeparam name="TError">The type of the error to return when the nullable value is not present.</typeparam>
        /// <param name="errorFactory">A function that produces an error of type TError to be returned if the nullable value is not present.</param>
        /// <returns>A ValueResult containing the value if present; otherwise, a ValueResult containing the error produced by
        /// errorFactory.</returns>
        public ValueResult<TValue, TError> ConvertToValueResult<TError>(Func<TError> errorFactory)
            => nullable.HasValue
                ? ValueResult<TValue, TError>.FromValue(nullable.Value)
                : ValueResult<TValue, TError>.FromError(errorFactory());
    }
}
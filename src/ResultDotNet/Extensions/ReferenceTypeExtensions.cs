#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;

public static class ReferenceTypeExtensions
{
    private const string ReferenceIsNullErrorMessage = "Reference is null.";

    extension<T>(T? obj)
        where T : class
    {
        /// <summary>
        /// Converts the current object to a result that contains either the object value or an error message if the
        /// object is null.
        /// </summary>
        /// <remarks>This method provides a safe way to wrap a reference type in a result, enabling error
        /// handling without throwing exceptions. It is useful for scenarios where null references should be treated as
        /// errors in a functional style.</remarks>
        /// <returns>A result containing the object value if it is not null; otherwise, a result containing an error message
        /// indicating that the reference is null.</returns>
        public Result<T, string> ConvertToResult()
            => obj is not null
                ? Result<T, string>.FromValue(obj)
                : Result<T, string>.FromError(ReferenceIsNullErrorMessage);

        /// <summary>
        /// Converts the current object to a result, returning a value if the object is not null, or an error if it is
        /// null.
        /// </summary>
        /// <remarks>Use this method to safely wrap a reference type in a result, distinguishing between
        /// success and failure states without throwing exceptions.</remarks>
        /// <typeparam name="TError">The type of the error value to return if the object is null.</typeparam>
        /// <param name="errorFactory">A function that creates an error value of type TError to be used when the object is null.</param>
        /// <returns>A Result<T, TError> containing the object if it is not null; otherwise, a result containing the error value
        /// produced by errorFactory.</returns>
        public Result<T, TError> ConvertToResult<TError>(Func<TError> errorFactory)
            => obj is not null
                ? Result<T, TError>.FromValue(obj)
                : Result<T, TError>.FromError(errorFactory());

        /// <summary>
        /// Converts the current object to a ValueResult containing either the object's value or an error message if the
        /// object is null.
        /// </summary>
        /// <remarks>Use this method to safely wrap a reference type in a ValueResult, enabling error
        /// handling for null references without throwing exceptions.</remarks>
        /// <returns>A ValueResult containing the value of the current object if it is not null; otherwise, a ValueResult
        /// containing an error message indicating that the reference is null.</returns>
        public ValueResult<T, string> ConvertToValueResult()
            => obj is not null
                ? ValueResult<T, string>.FromValue(obj)
                : ValueResult<T, string>.FromError(ReferenceIsNullErrorMessage);

        /// <summary>
        /// Converts the current object to a ValueResult, returning a value if the object is not null, or an error if it
        /// is null.
        /// </summary>
        /// <remarks>Use this method to safely wrap a potentially null object in a ValueResult, enabling
        /// error handling for null cases. The errorFactory function is only invoked if the object is null.</remarks>
        /// <typeparam name="TError">The type of the error value to return if the object is null.</typeparam>
        /// <param name="errorFactory">A function that creates an error value of type TError to be returned when the object is null.</param>
        /// <returns>A ValueResult containing the object as its value if it is not null; otherwise, a ValueResult containing the
        /// error produced by errorFactory.</returns>
        public ValueResult<T, TError> ConvertToValueResult<TError>(Func<TError> errorFactory)
            => obj is not null
                ? ValueResult<T, TError>.FromValue(obj)
                : ValueResult<T, TError>.FromError(errorFactory());
    }
}
namespace ResultDotNet;

/// <summary>
/// Represents the outcome of an operation that can either succeed with a value or fail with an error.
/// </summary>
/// <remarks>
/// Use this type to explicitly model operations that may fail, providing either a success value or an
/// error. This approach helps avoid exceptions for expected error conditions and enables clear handling of both success
/// and failure cases. The generic parameters allow flexibility in specifying the types for both successful results and
/// errors.
/// </remarks>
/// <typeparam name="TValue">The type of the value returned when the operation succeeds.</typeparam>
/// <typeparam name="TError">The type of the error returned when the operation fails.</typeparam>
public class Result<TValue, TError>
{
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result represents an error condition.
    /// </summary>
    public bool IsError => !IsSuccess;

    private Result(TValue value)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(TError error)
    {
        Error = error;
        IsSuccess = false;
    }

    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    /// <param name="value">The value to be wrapped in a successful result. Can be null if the type permits null values.</param>
    /// <returns>A Result object representing a successful outcome with the provided value.</returns>
    public static Result<TValue, TError> FromValue(TValue value) => new(value);

    /// <summary>
    /// Creates a new error result containing the specified error value.
    /// </summary>
    /// <param name="error">The error value to associate with the result. Cannot be null if the error type does not allow null values.</param>
    /// <returns>A result instance representing an error, containing the provided error value.</returns>
    public static Result<TValue, TError> FromError(TError error) => new(error);

    /// <summary>
    /// Gets the value contained in the result if the operation was successful.
    /// </summary>
    /// <remarks>
    /// Accessing this property when the result represents a failure will throw an exception. Use the
    /// <see cref="IsSuccess"/> property to check whether the result contains a value before accessing it.
    /// </remarks>
    public TValue Value
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException("Result does not contain a success value.");

            return field!;
        }
    }

    /// <summary>
    /// Gets the error value associated with the result when an error has occurred.
    /// </summary>
    /// <remarks>
    /// Accessing this property is only valid when the result represents an error. Attempting to
    /// retrieve the error value when no error is present will throw an exception.
    /// </remarks>
    public TError Error
    {
        get
        {
            if (!IsError)
                throw new InvalidOperationException("Result does not contain an error value.");

            return field!;
        }
    }
}
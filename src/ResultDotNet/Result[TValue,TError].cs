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

    /// <summary>
    /// Initializes a new instance of the Result class that represents a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value to associate with the successful result.</param>
    private Result(TValue value)
    {
        Value = value;
        IsSuccess = true;
    }

    /// <summary>
    /// Initializes a new instance of the Result class that represents a failed operation with the specified error.
    /// </summary>
    /// <param name="error">The error value that describes the reason for the failure. Cannot be null if TError is a reference type.</param>
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
    public static Result<TValue, TError> FromValue(TValue value) 
        => new(value);

    /// <summary>
    /// Converts a value of type <typeparamref name="TValue"/> to a <see cref="Result{TValue, TError}"/> representing a
    /// successful result.
    /// </summary>
    /// <remarks>This implicit conversion allows a value of type <typeparamref name="TValue"/> to be used
    /// directly where a <see cref="Result{TValue, TError}"/> is expected. The resulting <see cref="Result{TValue,
    /// TError}"/> will indicate success and contain the provided value.</remarks>
    /// <param name="value">The value to be wrapped in a successful <see cref="Result{TValue, TError}"/>.</param>
    public static implicit operator Result<TValue, TError>(TValue value) 
        => FromValue(value);

    /// <summary>
    /// Creates a new error result containing the specified error value.
    /// </summary>
    /// <param name="error">The error value to associate with the result. Cannot be null if the error type does not allow null values.</param>
    /// <returns>A result instance representing an error, containing the provided error value.</returns>
    public static Result<TValue, TError> FromError(TError error) 
        => new(error);

    /// <summary>
    /// Converts a value of type TError to a Result<TValue, TError> representing an error result.
    /// </summary>
    /// <remarks>This implicit conversion allows error values to be easily returned or assigned as
    /// Result<TValue, TError> instances, simplifying error handling scenarios.</remarks>
    /// <param name="error">The error value to be encapsulated in the Result object.</param>
    public static implicit operator Result<TValue, TError>(TError error) 
        => FromError(error);

    /// <summary>
    /// Gets the value contained in the result if the operation was successful.
    /// </summary>
    /// <remarks>
    /// Accessing this property when the result represents a failure will throw an exception. Use the
    /// <see cref="IsSuccess"/> property to check whether the result contains a value before accessing it.
    /// </remarks>
    public TValue Value => IsSuccess
        ? field!
        : throw new InvalidOperationException("Result does not contain a success value.");

    /// <summary>
    /// Gets the error value associated with the result when an error has occurred.
    /// </summary>
    /// <remarks>
    /// Accessing this property is only valid when the result represents an error. Attempting to
    /// retrieve the error value when no error is present will throw an exception.
    /// </remarks>
    public TError Error => IsError
        ? field!
        : throw new InvalidOperationException("Result does not contain an error value.");
}
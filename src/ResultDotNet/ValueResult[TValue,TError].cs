namespace ResultDotNet;

/// <summary>
/// Represents the result of an operation that can succeed or fail, encapsulating either a value or an error.
/// </summary>
/// <remarks>Use ValueResult<TValue, TError> to indicate the outcome of an operation without throwing exceptions
/// for expected error conditions. This type allows you to distinguish between successful and failed results and to
/// access error details when a failure occurs. It is commonly used in scenarios where operations may fail and you want
/// to avoid exception-based control flow.</remarks>
/// <typeparam name="TValue">The type of the value returned when the operation is successful.</typeparam>
/// <typeparam name="TError">The type of the error information provided when the operation fails.</typeparam>
public readonly struct ValueResult<TValue, TError>
{
    /// <summary>
    /// Gets a value indicating whether the result represents an error condition.
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Initializes a new instance of the ValueResult struct with a default successful state with the specified value.
    /// </summary>
    private ValueResult(TValue value)
    {
        Value = value;
        IsError = false;
    }

    /// <summary>
    /// Initializes a new instance of the ValueResult struct that represents an error result with the specified error
    /// value.
    /// </summary>
    /// <param name="error">The error value to associate with this result. This value indicates the reason for the error and must not be
    /// null.</param>
    private ValueResult(TError error)
    {
        Error = error;
        IsError = true;
    }

    /// <summary>
    /// Creates a new successful result containing the specified value.
    /// </summary>
    /// <param name="value">The value to store in the result. May be null if TValue is a reference type.</param>
    /// <returns>A ValueResult instance representing a successful result that contains the specified value.</returns>
    public static ValueResult<TValue, TError> FromValue(TValue value)
        => new(value);

    /// <summary>
    /// Converts a value of type TValue to a ValueResult<TValue, TError> representing a successful result.
    /// </summary>
    /// <param name="value">The value to be wrapped as a successful result.</param>
    public static implicit operator ValueResult<TValue, TError>(TValue value)
        => FromValue(value);

    /// <summary>
    /// Creates a new ValueResult<TValue, TError> that represents a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error value to associate with the failed result. Cannot be null if TError is a reference type.</param>
    /// <returns>A ValueResult<TValue, TError> instance representing a failure with the provided error value.</returns>
    public static ValueResult<TValue, TError> FromError(TError error) 
        => new(error);

    /// <summary>
    /// Creates a new error result from the specified error value.
    /// </summary>
    /// <param name="error">The error value to use for the result. Cannot be null if the error type is a reference type.</param>
    public static implicit operator ValueResult<TValue, TError>(TError error)
        => FromError(error);

    /// <summary>
    /// Gets the value contained in the result if the operation was successful.
    /// </summary>
    /// <remarks>Accessing this property when the result does not represent a successful operation will throw
    /// an exception. Use the IsSuccess property to determine whether a value is available before accessing this
    /// property.</remarks>
    public TValue Value => IsSuccess
        ? field!
        : throw new InvalidOperationException("ValueResult does not contain a success value.");

    /// <summary>
    /// Gets the error value contained in the result.
    /// </summary>
    /// <remarks>Accessing this property when the result does not represent an error will throw an exception.
    /// Use the IsError property to determine whether an error value is present before accessing this
    /// property.</remarks>
    public TError Error => IsError
        ? field!
        : throw new InvalidOperationException("ValueResult does not contain an error value.");
}
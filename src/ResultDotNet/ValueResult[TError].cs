namespace ResultDotNet;

/// <summary>
/// Represents the result of an operation that can either succeed or fail with an error value of a specified type.
/// </summary>
/// <remarks>Use ValueResult<TError> to indicate the success or failure of an operation without returning a value
/// on success. This struct is useful for scenarios where only error information is needed on failure, and no result
/// value is produced on success. The IsSuccess and IsError properties can be used to check the outcome of the
/// operation. If the result represents an error, the Error property provides the associated error value.</remarks>
/// <typeparam name="TError">The type of the error value returned when the operation fails.</typeparam>
public readonly struct ValueResult<TError>
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
    /// Initializes a new instance of the ValueResult struct with a default successful state.
    /// </summary>
    public ValueResult()
    {
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
    /// Creates a new successful instance of the ValueResult<TError> type.
    /// </summary>
    /// <returns>A ValueResult<TError> that represents a successful result with no associated error.</returns>
    public static ValueResult<TError> Success() 
        => new();

    /// <summary>
    /// Creates a new ValueResult<TError> that represents a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error value to associate with the failed result. Cannot be null if TError is a reference type.</param>
    /// <returns>A ValueResult<TError> instance representing a failure with the provided error value.</returns>
    public static ValueResult<TError> FromError(TError error) 
        => new(error);

    /// <summary>
    /// Creates a new <see cref="ValueResult{TError}"/> representing an error from the specified error value.
    /// </summary>
    /// <remarks>This implicit conversion allows an error value of type <typeparamref name="TError"/> to be
    /// automatically converted to a <see cref="ValueResult{TError}"/> representing an error result. This can simplify
    /// error handling by enabling direct assignment of error values where a <see cref="ValueResult{TError}"/> is
    /// expected.</remarks>
    /// <param name="error">The error value to encapsulate in the <see cref="ValueResult{TError}"/>.</param>
    public static implicit operator ValueResult<TError>(TError error) 
        => FromError(error);

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
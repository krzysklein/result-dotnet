namespace ResultDotNet;

/// <summary>
/// Represents the outcome of an operation, indicating success or providing an error value if the operation failed.
/// </summary>
/// <remarks>
/// Use the Result<TError> type to encapsulate the result of an operation that can either succeed or
/// fail, without throwing exceptions. When the operation succeeds, IsSuccess is <see langword="true"/> and no error
/// value is present. When the operation fails, IsError is <see langword="true"/> and the Error property contains the
/// error value. This type is useful for scenarios where you want to avoid exception-based error handling and instead
/// return explicit error information.
/// </remarks>
/// <typeparam name="TError">The type of the error value returned when the operation is unsuccessful.</typeparam>
public class Result<TError>
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
    /// Initializes a new instance of the Result class that represents a successful operation.
    /// </summary>
    private Result()
    {
        IsError = false;
    }

    /// <summary>
    /// Initializes a new instance of the Result class that represents a failed operation with the specified error.
    /// </summary>
    /// <param name="error">The error value that describes the reason for the failure. Cannot be null if TError is a reference type.</param>
    private Result(TError error)
    {
        Error = error;
        IsError = true;
    }

    /// <summary>
    /// Creates a new successful result with no error information.
    /// </summary>
    /// <returns>A <see cref="Result{TError}"/> instance representing a successful outcome.</returns>
    public static Result<TError> Success() 
        => new();

    /// <summary>
    /// Creates a new error result containing the specified error value.
    /// </summary>
    /// <param name="error">The error value to associate with the result. Cannot be null if the result type does not allow null errors.</param>
    /// <returns>A result instance representing an error with the provided error value.</returns>
    public static Result<TError> FromError(TError error) 
        => new(error);

    /// <summary>
    /// Creates a new <see cref="Result{TError}"/> instance representing an error from the specified error value.
    /// </summary>
    /// <remarks>This implicit conversion allows error values to be assigned directly to <see
    /// cref="Result{TError}"/> variables, simplifying error handling scenarios.</remarks>
    /// <param name="error">The error value to encapsulate in the <see cref="Result{TError}"/>.</param>
    public static implicit operator Result<TError>(TError error) 
        => FromError(error);

    /// <summary>
    /// Gets the error value associated with the result.
    /// </summary>
    /// <remarks>
    /// Accessing this property is only valid when the result represents an error. If the result does
    /// not contain an error, an <see cref="InvalidOperationException"/> is thrown.
    /// </remarks>
    public TError Error => IsError
        ? field!
        : throw new InvalidOperationException("Result does not contain an error value.");
}
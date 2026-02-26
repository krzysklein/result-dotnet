namespace ResultDotNet;

/// <summary>
/// Represents the result of an operation that indicates success or error without carrying additional data.
/// </summary>
/// <remarks>Use this struct to signal whether an operation completed successfully or encountered an error. The
/// <see cref="IsSuccess"/> and <see cref="IsError"/> properties provide convenient access to the result state. This
/// type is useful for scenarios where only the outcome is needed, without any associated value or error
/// details.</remarks>
public readonly struct ValueResult
{
    /// <summary>
    /// Gets a value indicating whether the current state represents an error condition.
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Initializes a new instance of the ValueResult struct with the specified error state.
    /// </summary>
    /// <param name="isError">A value indicating whether the result represents an error. Set to <see langword="true"/> if the result is an
    /// error; otherwise, <see langword="false"/>.</param>
    private ValueResult(bool isError)
    {
        IsError = isError;
    }

    /// <summary>
    /// Creates a new ValueResult instance that represents a successful operation.
    /// </summary>
    /// <returns>A ValueResult indicating success. The result will not contain an error.</returns>
    public static ValueResult Success()
        => new(isError: false);

    /// <summary>
    /// Creates a new ValueResult instance that represents an error state.
    /// </summary>
    /// <returns>A ValueResult object with its error flag set to indicate an error condition.</returns>
    public static ValueResult Error()
        => new(isError: true);
}
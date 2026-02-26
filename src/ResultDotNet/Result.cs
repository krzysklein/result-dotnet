namespace ResultDotNet;

/// <summary>
/// Represents the outcome of an operation, indicating whether it succeeded or failed.
/// </summary>
/// <remarks>Use the static methods <see cref="Success"/> and <see cref="Error"/> to create instances representing
/// successful or failed results. The <see cref="IsError"/> and <see cref="IsSuccess"/> properties provide convenient
/// access to the operation's status.</remarks>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the current result represents an error condition.
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Initializes a new instance of the Result class with the specified error state.
    /// </summary>
    /// <param name="isError">A value indicating whether the result represents an error. Set to <see langword="true"/> to indicate an error;
    /// otherwise, <see langword="false"/>.</param>
    private Result(bool isError)
    {
        IsError = isError;
    }

    /// <summary>
    /// Creates a new <see cref="Result"/> instance representing a successful outcome.
    /// </summary>
    /// <returns>A <see cref="Result"/> object indicating success.</returns>
    public static Result Success()
        => new(isError: false);

    /// <summary>
    /// Creates a new result that represents an error state.
    /// </summary>
    /// <returns>A <see cref="Result"/> instance indicating an error has occurred.</returns>
    public static Result Error()
        => new(isError: true);
}

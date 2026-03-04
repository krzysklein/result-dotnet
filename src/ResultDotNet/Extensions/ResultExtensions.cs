#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ResultExtensions
{
    extension(Result result)
    {
        /// <summary>
        /// Invokes the specified function if the current result represents success, and returns its result; otherwise,
        /// returns the current failed result.
        /// </summary>
        /// <remarks>This method enables chaining of operations that may fail, propagating the first
        /// failure encountered. If the current result is not successful, <paramref name="bindFunc"/> is not
        /// invoked.</remarks>
        /// <param name="bindFunc">A function to invoke if the current result is successful. The function should return a new result to
        /// propagate.</param>
        /// <returns>The result returned by <paramref name="bindFunc"/> if the current result is successful; otherwise, the
        /// current failed result.</returns>
        public Result Bind(Func<Result> bindFunc)
            => result.IsSuccess
                ? bindFunc()
                : result;

        /// <summary>
        /// Asynchronously invokes the specified function if the current result is successful, enabling further
        /// processing in a result-based workflow.
        /// </summary>
        /// <remarks>This method is typically used to chain asynchronous operations that depend on the
        /// success of a previous result. If the current result is not successful, the provided function is not invoked
        /// and the failure is propagated.</remarks>
        /// <param name="bindAsyncFunc">A function to execute if the current result indicates success. The function must return a Task that produces
        /// a Result.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the outcome of the provided function
        /// if the current result is successful; otherwise, the original failed result.</returns>
        public async Task<Result> BindAsync(Func<Task<Result>> bindAsyncFunc)
            => result.IsSuccess
                ? await bindAsyncFunc()
                : result;

        /// <summary>
        /// Maps the error value of the current result to a new error type using the specified mapping function.
        /// </summary>
        /// <remarks>If the current result is successful, the mapping function is not invoked and a
        /// successful result is returned. This method is useful for converting error types in result chains.</remarks>
        /// <typeparam name="TError">The type of the error value to map to.</typeparam>
        /// <param name="mapFunc">A function that provides the new error value when the result represents a failure.</param>
        /// <returns>A result containing the mapped error value if the current result is a failure; otherwise, a successful
        /// result of the new error type.</returns>
        public Result<TError> MapError<TError>(Func<TError> mapFunc)
            => result.IsSuccess
                ? Result<TError>.Success()
                : Result<TError>.FromError(mapFunc());

        /// <summary>
        /// Asynchronously maps the error value of the current result to a new error type using the specified mapping
        /// function.
        /// </summary>
        /// <remarks>If the current result is successful, the mapping function is not invoked and a
        /// successful result is returned. This method is useful for transforming error information in asynchronous
        /// workflows.</remarks>
        /// <typeparam name="TError">The type to which the error value will be mapped.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously produces the new error value when the current result represents a failure.
        /// Cannot be null.</param>
        /// <returns>A result containing the mapped error value if the current result is a failure; otherwise, a successful
        /// result of the new error type.</returns>
        public async Task<Result<TError>> MapErrorAsync<TError>(Func<Task<TError>> mapAsyncFunc)
            => result.IsSuccess
                ? Result<TError>.Success()
                : Result<TError>.FromError(await mapAsyncFunc());

        /// <summary>
        /// Invokes the specified callback based on the success or failure of the operation result.
        /// </summary>
        /// <remarks>Use this method to handle both success and error outcomes by providing appropriate
        /// actions for each case. This approach enables clear separation of logic for successful and failed
        /// operations.</remarks>
        /// <param name="onSuccess">The action to execute if the operation result indicates success. Cannot be null.</param>
        /// <param name="onError">The action to execute if the operation result indicates failure. Cannot be null.</param>
        public void Match(Action onSuccess, Action onError)
        {
            if (result.IsSuccess)
            {
                onSuccess();
            }
            else
            {
                onError();
            }
        }

        /// <summary>
        /// Invokes the specified delegate based on the result state and returns its value.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a single expression,
        /// enabling functional-style result processing.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccess">A delegate to invoke and return its result if the operation was successful. Cannot be null.</param>
        /// <param name="onError">A delegate to invoke and return its result if the operation failed. Cannot be null.</param>
        /// <returns>The value returned by either the <paramref name="onSuccess"/> or <paramref name="onError"/> delegate,
        /// depending on the result state.</returns>
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<TResult> onError)
            => result.IsSuccess
                ? onSuccess()
                : onError();

        /// <summary>
        /// Executes the specified action if the result is successful; otherwise, executes the provided asynchronous
        /// error handler.
        /// </summary>
        /// <remarks>Use this method to separate success and error handling logic when working with
        /// result-based operations. The method ensures that error handling can be performed asynchronously, which is
        /// useful for scenarios such as logging, user notifications, or cleanup tasks that may require asynchronous
        /// execution.</remarks>
        /// <param name="onSuccess">The action to execute when the result indicates success. This delegate is invoked synchronously.</param>
        /// <param name="onErrorAsync">The asynchronous function to execute when the result indicates failure. This delegate is awaited.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task MatchAsync(Action onSuccess, Func<Task> onErrorAsync)
        {
            if (result.IsSuccess)
            {
                onSuccess();
            }
            else
            {
                await onErrorAsync();
            }
        }

        /// <summary>
        /// Invokes the specified delegate based on the result state and returns a value of the specified type
        /// asynchronously.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a unified way, returning a
        /// value of the same type for either outcome. The onSuccess delegate is executed synchronously, while
        /// onErrorAsync is awaited if the operation failed.</remarks>
        /// <typeparam name="TResult">The type of the value to return from the delegate.</typeparam>
        /// <param name="onSuccess">A delegate to invoke and return its result if the operation was successful. Cannot be null.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to invoke and return its result if the operation failed. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// onSuccess or onErrorAsync delegate, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> onSuccess, Func<Task<TResult>> onErrorAsync)
            => result.IsSuccess
                ? onSuccess()
                : await onErrorAsync();

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful; otherwise, executes the error
        /// action.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a result-driven workflow.
        /// The appropriate action is invoked based on the result state. Ensure that both delegates are provided and
        /// handle any exceptions that may occur within them.</remarks>
        /// <param name="onSuccessAsync">A function that represents the asynchronous operation to perform when the result indicates success. Cannot
        /// be null.</param>
        /// <param name="onError">An action to execute if the result indicates failure. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous execution of the success or error action.</returns>
        public async Task MatchAsync(Func<Task> onSuccessAsync, Action onError)
        {
            if (result.IsSuccess)
            {
                await onSuccessAsync();
            }
            else
            {
                onError();
            }
        }

        /// <summary>
        /// Asynchronously executes one of the specified delegates based on whether the result represents success or
        /// failure, and returns the corresponding value.
        /// </summary>
        /// <remarks>If the result is successful, the onSuccessAsync delegate is invoked and awaited. If
        /// the result is an error, the onError delegate is invoked synchronously. Neither delegate may be
        /// null.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result is successful. The function returns a task that produces a
        /// value of type TResult.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function returns a value of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the value returned by either the
        /// onSuccessAsync or onError delegate, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> onSuccessAsync, Func<TResult> onError)
            => result.IsSuccess
                ? await onSuccessAsync()
                : onError();

        /// <summary>
        /// Executes the specified asynchronous action depending on whether the operation was successful or not.
        /// </summary>
        /// <remarks>Use this method to handle success and error scenarios with custom asynchronous logic.
        /// Both callbacks must be non-null and should handle exceptions internally to avoid unobserved task
        /// exceptions.</remarks>
        /// <param name="onSuccessAsync">The asynchronous callback to invoke if the operation is successful. This function should perform any actions
        /// required upon success.</param>
        /// <param name="onErrorAsync">The asynchronous callback to invoke if the operation fails. This function should handle any error-related
        /// actions.</param>
        /// <returns>A task that represents the asynchronous execution of the appropriate callback based on the operation's
        /// result.</returns>
        public async Task MatchAsync(Func<Task> onSuccessAsync, Func<Task> onErrorAsync)
        {
            if (result.IsSuccess)
            {
                await onSuccessAsync();
            }
            else
            {
                await onErrorAsync();
            }
        }

        /// <summary>
        /// Asynchronously invokes one of the specified functions based on whether the result represents success or
        /// error, and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the asynchronous functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents success. The function returns a value of type
        /// <typeparamref name="TResult"/>.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result represents an error. The function returns a value of type
        /// <typeparamref name="TResult"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either
        /// <paramref name="onSuccessAsync"/> or <paramref name="onErrorAsync"/>, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> onSuccessAsync, Func<Task<TResult>> onErrorAsync)
            => result.IsSuccess
                ? await onSuccessAsync()
                : await onErrorAsync();
    }
}
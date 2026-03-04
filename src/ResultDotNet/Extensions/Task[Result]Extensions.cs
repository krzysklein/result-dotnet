#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class TaskOfResultExtensions
{
    extension(Task<Result> resultAsync)
    {
        /// <summary>
        /// Invokes the specified function if the asynchronous result is successful, and returns the result of that
        /// function as a new asynchronous operation.
        /// </summary>
        /// <param name="bindFunc">A function to execute if the result is successful. The function should return a new result to be used as the
        /// outcome of the operation.</param>
        /// <returns>A task that represents the asynchronous bind operation. The task result contains the outcome of the bind
        /// function if the original result is successful; otherwise, it contains the original failure.</returns>
        public async Task<Result> BindAsync(Func<Result> bindFunc)
            => (await resultAsync).Bind(bindFunc);

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error type using the specified mapping function.
        /// </summary>
        /// <typeparam name="TError">The type to which the error value will be mapped.</typeparam>
        /// <param name="mapFunc">A function that provides the new error value to map to. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the error
        /// value mapped to the specified type.</returns>
        public async Task<Result<TError>> MapErrorAsync<TError>(Func<TError> mapFunc)
            => (await resultAsync).MapError(mapFunc);

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error using the specified asynchronous mapping
        /// function.
        /// </summary>
        /// <typeparam name="TError">The type of the error value returned by the mapping function.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously produces a new error value to replace the current error.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a result with the mapped error
        /// value.</returns>
        public async Task<Result<TError>> MapErrorAsync<TError>(Func<Task<TError>> mapAsyncFunc)
            => await (await resultAsync).MapErrorAsync(mapAsyncFunc);

        /// <summary>
        /// Asynchronously executes the specified handler based on the outcome of the operation, invoking the success
        /// action if the operation completes successfully or the error handler if it fails.
        /// </summary>
        /// <remarks>This method awaits the result of an asynchronous operation and invokes the
        /// appropriate handler depending on whether the operation succeeded or failed. Ensure that both handlers are
        /// provided and capable of handling the expected outcomes.</remarks>
        /// <param name="onSuccess">An action to execute when the operation completes successfully.</param>
        /// <param name="onErrorAsync">A function that returns a task to execute when the operation fails.</param>
        /// <returns>A task that represents the asynchronous match operation.</returns>
        public async Task MatchAsync(Action onSuccess, Func<Task> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccess, onErrorAsync);

        /// <summary>
        /// Invokes the specified delegate based on the result state, returning a value of the specified type
        /// asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccess">A delegate to invoke if the result represents a success. The delegate returns a value of type TResult.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to invoke if the result represents an error. The delegate returns a Task that
        /// produces a value of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// onSuccess or onErrorAsync delegate, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> onSuccess, Func<Task<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccess, onErrorAsync);

        /// <summary>
        /// Asynchronously invokes the specified handler based on the outcome of the operation, executing the success
        /// handler if the operation succeeds or the error handler if it fails.
        /// </summary>
        /// <remarks>This method awaits the result of an asynchronous operation and calls the appropriate
        /// handler depending on whether the operation was successful or encountered an error. Ensure that the provided
        /// handlers are suitable for the expected outcomes.</remarks>
        /// <param name="onSuccessAsync">A function that is executed asynchronously if the operation completes successfully.</param>
        /// <param name="onError">An action that is executed if the operation results in an error.</param>
        /// <returns>A task that represents the asynchronous match operation.</returns>
        public async Task MatchAsync(Func<Task> onSuccessAsync, Action onError)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onError);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on the result state and returns a value of the specified
        /// type.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents a success. The function returns a task that
        /// produces the result value.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function returns the result value directly.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// success or error delegate, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> onSuccessAsync, Func<TResult> onError)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onError);

        /// <summary>
        /// Asynchronously invokes the specified callback based on the outcome of the operation represented by this
        /// result.
        /// </summary>
        /// <remarks>This method awaits the completion of the underlying result and then calls either the
        /// success or error callback, depending on the result. Both callbacks are executed asynchronously.</remarks>
        /// <param name="onSuccessAsync">A function to execute if the operation completes successfully. The function should return a task
        /// representing the asynchronous work to perform on success.</param>
        /// <param name="onErrorAsync">A function to execute if the operation fails. The function should return a task representing the
        /// asynchronous work to perform on error.</param>
        /// <returns>A task that represents the asynchronous matching operation.</returns>
        public async Task MatchAsync(Func<Task> onSuccessAsync, Func<Task> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onErrorAsync);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents a success or an error,
        /// and returns the corresponding value.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the success or error delegate.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents a success. The function returns a value of type
        /// TResult.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result represents an error. The function returns a value of type
        /// TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// success or error delegate, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> onSuccessAsync, Func<Task<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onErrorAsync);
    }
}
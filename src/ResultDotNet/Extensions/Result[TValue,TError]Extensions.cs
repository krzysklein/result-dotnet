#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ResultOfTValueTErrorExtensions
{
    extension<TValue, TError>(Result<TValue, TError> result)
    {
        /// <summary>
        /// Invokes the specified function on the successful result value and returns its result, or propagates the
        /// error if the result is unsuccessful.
        /// </summary>
        /// <remarks>This method enables chaining of operations that may fail, commonly used in functional
        /// programming patterns such as monadic binding. If the original result is unsuccessful, the bind function is
        /// not invoked and the error is returned.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the bind function.</typeparam>
        /// <param name="bindFunc">A function to apply to the value if the result is successful. The function should return a new result of
        /// type <typeparamref name="TValue2"/>.</param>
        /// <returns>A result containing the value returned by <paramref name="bindFunc"/> if the original result is successful;
        /// otherwise, a result containing the propagated error.</returns>
        public Result<TValue2, TError> Bind<TValue2>(Func<TValue, Result<TValue2, TError>> bindFunc)
            => result.IsSuccess
                ? bindFunc(result.Value)
                : Result<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Asynchronously applies the specified binding function to the successful result value, returning a new result
        /// of the specified type.
        /// </summary>
        /// <remarks>This method enables chaining asynchronous operations on result values, commonly used
        /// for composing tasks that may fail. If the current result is unsuccessful, the binding function is not called
        /// and the error is returned immediately.</remarks>
        /// <typeparam name="TValue2">The type of the value contained in the result returned by the binding function.</typeparam>
        /// <param name="bindAsyncFunc">A function that takes the current result value and returns a task representing the asynchronous operation to
        /// produce a new result.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result of type TValue2 and
        /// TError. If the current result is unsuccessful, the error is propagated without invoking the binding
        /// function.</returns>
        public async Task<Result<TValue2, TError>> BindAsync<TValue2>(Func<TValue, Task<Result<TValue2, TError>>> bindAsyncFunc)
            => result.IsSuccess
                ? await bindAsyncFunc(result.Value)
                : Result<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Transforms the successful value of the result to a new type using the specified mapping function.
        /// </summary>
        /// <remarks>If the result represents an error, the mapping function is not invoked and the error
        /// is propagated. This method is useful for chaining transformations while preserving error
        /// information.</remarks>
        /// <typeparam name="TValue2">The type to which the successful value is mapped.</typeparam>
        /// <param name="mapFunc">A function that takes the current successful value and returns a value of type <typeparamref
        /// name="TValue2"/>. Cannot be null.</param>
        /// <returns>A new result containing the mapped value if the original result is successful; otherwise, a result
        /// containing the original error.</returns>
        public Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> mapFunc)
            => result.IsSuccess
                ? Result<TValue2, TError>.FromValue(mapFunc(result.Value))
                : Result<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Asynchronously transforms the successful value of the result using the specified mapping function.
        /// </summary>
        /// <remarks>If the result is not successful, the mapping function is not invoked and the error is
        /// propagated. The returned task completes when the mapping function's task completes.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the asynchronous mapping function.</typeparam>
        /// <param name="mapAsyncFunc">A function that takes the current value and returns a task representing the asynchronous transformation.
        /// Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the
        /// transformed value if the original result was successful; otherwise, it contains the original error.</returns>
        public async Task<Result<TValue2, TError>> MapAsync<TValue2>(Func<TValue, Task<TValue2>> mapAsyncFunc)
            => result.IsSuccess
                ? Result<TValue2, TError>.FromValue(await mapAsyncFunc(result.Value))
                : Result<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Transforms the error value of the result to a new error type using the specified mapping function.
        /// </summary>
        /// <remarks>If the result is successful, the error mapping function is not invoked. This method
        /// is useful for adapting error types when composing operations.</remarks>
        /// <typeparam name="TError2">The type to which the error value will be mapped.</typeparam>
        /// <param name="mapFunc">A function that takes the current error value and returns a new error value of type <typeparamref
        /// name="TError2"/>.</param>
        /// <returns>A result containing the original value if the operation was successful; otherwise, a result containing the
        /// mapped error value.</returns>
        public Result<TValue, TError2> MapError<TError2>(Func<TError, TError2> mapFunc)
            => result.IsSuccess
                ? Result<TValue, TError2>.FromValue(result.Value)
                : Result<TValue, TError2>.FromError(mapFunc(result.Error));

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error type using the specified mapping function.
        /// </summary>
        /// <remarks>If the result is successful, the mapping function is not invoked and the value is
        /// preserved. This method is useful for adapting error types in asynchronous workflows.</remarks>
        /// <typeparam name="TError2">The type to which the error value will be mapped.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously transforms the error value to a new error type. The function is invoked only
        /// if the result represents an error.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the same value
        /// if successful, or the mapped error if not.</returns>
        public async Task<Result<TValue, TError2>> MapErrorAsync<TError2>(Func<TError, Task<TError2>> mapAsyncFunc)
            => result.IsSuccess
                ? Result<TValue, TError2>.FromValue(result.Value)
                : Result<TValue, TError2>.FromError(await mapAsyncFunc(result.Error));

        /// <summary>
        /// Invokes the specified callback based on whether the result represents a success or an error.
        /// </summary>
        /// <remarks>Use this method to handle success and error cases separately by providing distinct
        /// actions for each scenario. The appropriate callback is executed depending on the state of the
        /// result.</remarks>
        /// <param name="onSuccess">A function to execute if the result is successful. This callback is invoked when the operation completes
        /// without error.</param>
        /// <param name="onError">A function to execute if the result is an error. This callback is invoked when the operation fails.</param>
        public void Match(Action<TValue> onSuccess, Action<TError> onError)
        {
            if (result.IsSuccess)
            {
                onSuccess(result.Value);
            }
            else
            {
                onError(result.Error);
            }
        }

        /// <summary>
        /// Invokes the appropriate callback based on whether the result represents a success or an error, and returns
        /// the value produced by the callback.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a functional style,
        /// ensuring that all possible result states are processed.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the callback functions.</typeparam>
        /// <param name="onSuccess">A function to invoke if the result is successful. Receives the success value and returns a value of type
        /// <typeparamref name="TResult"/>.</param>
        /// <param name="onError">A function to invoke if the result represents an error. Receives the error value and returns a value of type
        /// <typeparamref name="TResult"/>.</param>
        /// <returns>The value returned by either the <paramref name="onSuccess"/> or <paramref name="onError"/> callback,
        /// depending on the result state.</returns>
        public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onError)
            => result.IsSuccess
                ? onSuccess(result.Value)
                : onError(result.Error);

        /// <summary>
        /// Executes the specified action for a successful result or asynchronously handles an error for a failed
        /// result.
        /// </summary>
        /// <remarks>Use this method to provide custom handling for both success and error cases in an
        /// asynchronous workflow. The method ensures that only one of the provided delegates is executed, depending on
        /// the result state.</remarks>
        /// <param name="onSuccess">An action to invoke if the result is successful. The action receives the result value as its parameter.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result is an error. The function receives the error value as its
        /// parameter and returns a task representing the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task MatchAsync(Action<TValue> onSuccess, Func<TError, Task> onErrorAsync)
        {
            if (result.IsSuccess)
            {
                onSuccess(result.Value);
            }
            else
            {
                await onErrorAsync(result.Error);
            }
        }

        /// <summary>
        /// Invokes the specified delegate based on the result state, returning a value of type TResult. If the result
        /// is successful, executes the synchronous onSuccess delegate; otherwise, executes the asynchronous
        /// onErrorAsync delegate.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a result, allowing for
        /// asynchronous error handling while keeping success handling synchronous. The method ensures that only one of
        /// the provided delegates is invoked, depending on the result state.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates and the method.</typeparam>
        /// <param name="onSuccess">A delegate to execute if the result is successful. Receives the successful value and returns a TResult.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to execute if the result is an error. Receives the error value and returns a Task
        /// containing a TResult.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the value returned by the
        /// invoked delegate.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TValue, TResult> onSuccess, Func<TError, Task<TResult>> onErrorAsync)
            => result.IsSuccess
                ? onSuccess(result.Value)
                : await onErrorAsync(result.Error);

        /// <summary>
        /// Executes the specified asynchronous action if the operation is successful; otherwise, invokes the error
        /// handler.
        /// </summary>
        /// <remarks>This method allows for handling both success and error cases in an asynchronous
        /// manner, enabling a clear separation of success and error handling logic.</remarks>
        /// <param name="onSuccessAsync">A function that is called with the successful result value when the operation completes successfully.</param>
        /// <param name="onError">An action that is invoked with the error value when the operation fails.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task MatchAsync(Func<TValue, Task> onSuccessAsync, Action<TError> onError)
        {
            if (result.IsSuccess)
            {
                await onSuccessAsync(result.Value);
            }
            else
            {
                onError(result.Error);
            }
        }

        /// <summary>
        /// Asynchronously invokes the specified callback based on whether the result represents success or error.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a unified manner. The
        /// appropriate callback is invoked depending on the result state.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the callbacks.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result is successful. The function receives the success value and
        /// returns a task that produces the result.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function receives the error value and returns
        /// the result.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the value returned by either the
        /// success or error callback.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onSuccessAsync, Func<TError, TResult> onError)
            => result.IsSuccess
                ? await onSuccessAsync(result.Value)
                : onError(result.Error);

        /// <summary>
        /// Invokes the specified asynchronous callback based on whether the result represents a success or an error.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in an asynchronous manner,
        /// enabling responsive and non-blocking workflows. The appropriate callback is invoked depending on the state
        /// of the result.</remarks>
        /// <param name="onSuccessAsync">A function to execute asynchronously if the result is successful. The function receives the result value as
        /// its argument.</param>
        /// <param name="onErrorAsync">A function to execute asynchronously if the result is an error. The function receives the error value as its
        /// argument.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task MatchAsync(Func<TValue, Task> onSuccessAsync, Func<TError, Task> onErrorAsync)
        {
            if (result.IsSuccess)
            {
                await onSuccessAsync(result.Value);
            }
            else
            {
                await onErrorAsync(result.Error);
            }
        }

        /// <summary>
        /// Asynchronously matches the result by invoking the appropriate callback for success or error cases.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the callback functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents a success. The function receives the success
        /// value and returns a task producing the result.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result represents an error. The function receives the error value
        /// and returns a task producing the result.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by the
        /// invoked callback.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onSuccessAsync, Func<TError, Task<TResult>> onErrorAsync)
            => result.IsSuccess
                ? await onSuccessAsync(result.Value)
                : await onErrorAsync(result.Error);
    }
}
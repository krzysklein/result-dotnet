#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class TaskOfResultOfTErrorExtensions
{
    extension<TError>(Task<Result<TError>> resultAsync)
    {
        /// <summary>
        /// Asynchronously applies the specified binding function to the result if it represents a success, returning a
        /// new result of the specified type.
        /// </summary>
        /// <typeparam name="TValue2">The type of the value contained in the result returned by the binding function.</typeparam>
        /// <param name="bindFunc">A function to invoke if the current result is successful. The function returns a new result of type TValue2
        /// with the same error type.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result produced by
        /// applying the binding function if the original result is successful; otherwise, it contains the original
        /// error.</returns>
        public async Task<Result<TValue2, TError>> BindAsync<TValue2>(Func<Result<TValue2, TError>> bindFunc)
            => (await resultAsync).Bind(bindFunc);

        /// <summary>
        /// Asynchronously applies the specified binding function to the result, returning a new result of the specified
        /// type.
        /// </summary>
        /// <remarks>If the current result is a failure, the binding function is not invoked and the error
        /// is propagated. This method enables chaining of asynchronous operations that return results.</remarks>
        /// <typeparam name="TValue2">The type of the value contained in the result returned by the binding function.</typeparam>
        /// <param name="bindAsyncFunc">A function that returns a task representing the asynchronous operation to bind to the current result. The
        /// function is invoked if the current result is successful.</param>
        /// <returns>A task that represents the asynchronous bind operation. The task result contains a result of type TValue2
        /// and the same error type.</returns>
        public async Task<Result<TValue2, TError>> BindAsync<TValue2>(Func<Task<Result<TValue2, TError>>> bindAsyncFunc)
            => await (await resultAsync).BindAsync(bindAsyncFunc);

        /// <summary>
        /// Asynchronously transforms the successful result value to a new value using the specified mapping function.
        /// </summary>
        /// <typeparam name="TValue2">The type of the value returned by the mapping function.</typeparam>
        /// <param name="mapFunc">A function to apply to the successful result value to produce a new value. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new Result object with the
        /// mapped value if the original result was successful; otherwise, contains the original error.</returns>
        public async Task<Result<TValue2, TError>> MapAsync<TValue2>(Func<TValue2> mapFunc)
            => (await resultAsync).Map(mapFunc);

        /// <summary>
        /// Asynchronously transforms the successful result value to a new value using the specified asynchronous
        /// mapping function.
        /// </summary>
        /// <remarks>If the original result represents an error, the mapping function is not invoked and
        /// the error is propagated. This method enables chaining of asynchronous operations on result values.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the mapping function and contained in the resulting result.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously produces a new value to replace the current successful result value. Cannot
        /// be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the mapped
        /// value if the original result was successful; otherwise, contains the original error.</returns>
        public async Task<Result<TValue2, TError>> MapAsync<TValue2>(Func<Task<TValue2>> mapAsyncFunc)
            => await (await resultAsync).MapAsync(mapAsyncFunc);

        /// <summary>
        /// Asynchronously transforms the error value of the result using the specified mapping function.
        /// </summary>
        /// <typeparam name="TError2">The type of the error value returned by the mapping function.</typeparam>
        /// <param name="mapFunc">A function to apply to the error value if the result represents a failure. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the error
        /// value mapped to the specified type, or the original success value if the result was successful.</returns>
        public async Task<Result<TError2>> MapErrorAsync<TError2>(Func<TError, TError2> mapFunc)
            => (await resultAsync).MapError(mapFunc);

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error type using the specified asynchronous
        /// mapping function.
        /// </summary>
        /// <typeparam name="TError2">The type of the error value returned by the mapping function.</typeparam>
        /// <param name="mapAsyncFunc">A function that takes the current error value and returns a task that produces the mapped error value.
        /// Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the mapped
        /// error value if the original result is an error; otherwise, the original successful result.</returns>
        public async Task<Result<TError2>> MapErrorAsync<TError2>(Func<TError, Task<TError2>> mapAsyncFunc)
            => await (await resultAsync).MapErrorAsync(mapAsyncFunc);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on the result state, returning a value of the specified
        /// type.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a unified way, returning a
        /// value regardless of the result state. The onErrorAsync delegate is awaited if the result is an
        /// error.</remarks>
        /// <typeparam name="TResult">The type of the value to return from the match operation.</typeparam>
        /// <param name="onSuccess">A delegate to invoke if the result represents a success. The delegate returns a value of type TResult.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to invoke if the result represents an error. The delegate receives the error value
        /// and returns a task that produces a value of type TResult.</param>
        /// <returns>A task that represents the asynchronous match operation. The task result is the value returned by either the
        /// onSuccess or onErrorAsync delegate, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> onSuccess, Func<TError, Task<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccess, onErrorAsync);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on the result state, returning a value of type TResult.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents success. The function returns a Task that
        /// produces a value of type TResult.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function receives the error value and returns a
        /// TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either
        /// onSuccessAsync or onError, depending on the result state.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> onSuccessAsync, Func<TError, TResult> onError)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onError);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents a success or an error.
        /// </summary>
        /// <remarks>Both delegate parameters must not be null. The appropriate delegate is invoked
        /// depending on the state of the result. This method enables asynchronous pattern matching on the
        /// result.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result is successful. The function returns a value of type
        /// TResult.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result represents an error. The function receives the error value
        /// and returns a value of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by the
        /// invoked delegate.</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> onSuccessAsync, Func<TError, Task<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onErrorAsync);
    }
}
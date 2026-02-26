#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ValueTaskOfValueResultExtensions
{
    extension(ValueTask<ValueResult> resultAsync)
    {
        /// <summary>
        /// Invokes the specified binding function if the asynchronous result is successful, returning the result of the
        /// binding operation.
        /// </summary>
        /// <param name="bindFunc">A function to invoke if the asynchronous result is successful. The function should return a new ValueResult
        /// instance.</param>
        /// <returns>A ValueTask containing the result of the binding operation. If the asynchronous result is successful, the
        /// returned ValueResult is produced by the binding function; otherwise, the original failure is propagated.</returns>
        public async ValueTask<ValueResult> BindAsync(Func<ValueResult> bindFunc)
            => (await resultAsync).Bind(bindFunc);

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error type using the specified mapping function.
        /// </summary>
        /// <typeparam name="TError">The type to which the error value will be mapped.</typeparam>
        /// <param name="mapFunc">A function that provides the new error value to map to. Cannot be null.</param>
        /// <returns>A ValueTask that represents the asynchronous operation. The result contains a ValueResult with the mapped
        /// error value if the original result is an error; otherwise, the original success value is preserved.</returns>
        public async ValueTask<ValueResult<TError>> MapErrorAsync<TError>(Func<TError> mapFunc)
            => (await resultAsync).MapError(mapFunc);

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error using the specified asynchronous mapping
        /// function.
        /// </summary>
        /// <remarks>If the result is successful, the mapping function is not invoked and the original
        /// value is returned. If the result contains an error, the mapping function is called to produce a new error
        /// value. This method is useful for transforming error values in asynchronous workflows.</remarks>
        /// <typeparam name="TError">The type of the error value returned by the mapping function.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously produces a new error value to replace the current error.</param>
        /// <returns>A ValueTask that represents the asynchronous operation. The result contains a ValueResult with the mapped
        /// error value if an error is present; otherwise, the original successful value.</returns>
        public async ValueTask<ValueResult<TError>> MapErrorAsync<TError>(Func<ValueTask<TError>> mapAsyncFunc)
            => await (await resultAsync).MapErrorAsync(mapAsyncFunc);

        /// <summary>
        /// Asynchronously invokes the appropriate delegate based on the result state and returns a value of the
        /// specified type.
        /// </summary>
        /// <remarks>If the result is successful, <paramref name="onSuccess"/> is invoked synchronously.
        /// If the result is an error, <paramref name="onErrorAsync"/> is invoked asynchronously. This method allows for
        /// handling both success and error cases with different delegate types.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccess">A delegate to invoke and return its result if the operation was successful.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to invoke and return its result if the operation resulted in an error.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// <paramref name="onSuccess"/> or <paramref name="onErrorAsync"/> delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> onSuccess, Func<ValueTask<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccess, onErrorAsync);

        /// <summary>
        /// Asynchronously invokes the appropriate delegate based on the result state and returns the corresponding
        /// value.
        /// </summary>
        /// <remarks>If the result is successful, <paramref name="onSuccessAsync"/> is invoked
        /// asynchronously; otherwise, <paramref name="onError"/> is invoked synchronously. Neither delegate should be
        /// null.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccessAsync">A delegate to invoke asynchronously if the result represents a success. The delegate returns a value of type
        /// <typeparamref name="TResult"/>.</param>
        /// <param name="onError">A delegate to invoke if the result represents an error. The delegate returns a value of type <typeparamref
        /// name="TResult"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either
        /// <paramref name="onSuccessAsync"/> or <paramref name="onError"/>, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> onSuccessAsync, Func<TResult> onError)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onError);

        /// <summary>
        /// Asynchronously invokes the appropriate delegate based on whether the result represents a success or an
        /// error, and returns the delegate's result.
        /// </summary>
        /// <remarks>The appropriate delegate is invoked based on the current state of the result. Only
        /// one of the delegates will be called. Both delegates must not be null.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the success or error delegate.</typeparam>
        /// <param name="onSuccessAsync">A delegate to invoke asynchronously if the result represents a success. The delegate returns a value of type
        /// TResult.</param>
        /// <param name="onErrorAsync">A delegate to invoke asynchronously if the result represents an error. The delegate returns a value of type
        /// TResult.</param>
        /// <returns>A ValueTask that represents the asynchronous operation. The task result is the value returned by either the
        /// success or error delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> onSuccessAsync, Func<ValueTask<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onErrorAsync);
    }
}
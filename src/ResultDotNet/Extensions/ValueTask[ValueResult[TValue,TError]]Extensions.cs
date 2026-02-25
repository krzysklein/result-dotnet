#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ValueTaskOfValueResultOfTValueTErrorExtensions
{
    extension<TValue, TError>(ValueTask<ValueResult<TValue, TError>> resultAsync)
    {
        /// <summary>
        /// Asynchronously applies the specified binding function to the result value if the asynchronous operation
        /// completes successfully.
        /// </summary>
        /// <remarks>This method enables chaining of asynchronous result-producing operations, allowing
        /// for composition of dependent computations that may fail. If the original asynchronous result is a failure,
        /// the binding function is not invoked and the error is propagated.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the binding function and the resulting asynchronous operation.</typeparam>
        /// <param name="bindFunc">A function to apply to the result value if the asynchronous operation is successful. The function should
        /// return a new result of type <see cref="Result{TValue2, TError}"/>.</param>
        /// <returns>A task that represents the asynchronous bind operation. The task result contains a <see
        /// cref="Result{TValue2, TError}"/> produced by applying <paramref name="bindFunc"/> to the successful result
        /// value, or propagates the error if the original operation failed.</returns>
        public async ValueTask<ValueResult<TValue2, TError>> BindAsync<TValue2>(Func<TValue, ValueResult<TValue2, TError>> bindFunc)
            => (await resultAsync).Bind(bindFunc);

        /// <summary>
        /// Asynchronously applies the specified binding function to the result value, if the result represents success,
        /// and returns a new result containing the outcome of the asynchronous operation.
        /// </summary>
        /// <remarks>If the original result represents an error, the binding function is not invoked and
        /// the error is propagated. This method enables chaining of asynchronous operations that may fail, following
        /// the monadic bind pattern.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the asynchronous binding function.</typeparam>
        /// <param name="bindAsyncFunc">A function to invoke if the result is successful. The function receives the successful value and returns a
        /// task that produces a new result.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result of type TValue2 if
        /// the binding function is applied successfully; otherwise, it contains the original error.</returns>
        public async ValueTask<ValueResult<TValue2, TError>> BindAsync<TValue2>(Func<TValue, ValueTask<ValueResult<TValue2, TError>>> bindAsyncFunc)
            => await (await resultAsync).BindAsync(bindAsyncFunc);

        /// <summary>
        /// Asynchronously transforms the successful result value using the specified mapping function.
        /// </summary>
        /// <typeparam name="TValue2">The type of the value returned by the mapping function.</typeparam>
        /// <param name="mapFunc">A function to apply to the successful result value. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the mapped
        /// value if the original result was successful; otherwise, the original error.</returns>
        public async ValueTask<ValueResult<TValue2, TError>> MapAsync<TValue2>(Func<TValue, TValue2> mapFunc)
            => (await resultAsync).Map(mapFunc);

        /// <summary>
        /// Asynchronously transforms the successful result value using the specified asynchronous mapping function.
        /// </summary>
        /// <typeparam name="TValue2">The type of the value returned by the asynchronous mapping function.</typeparam>
        /// <param name="mapAsyncFunc">A function to apply to the successful result value. The function accepts the current value and returns a
        /// task that produces the mapped value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the mapped
        /// value if the original result was successful; otherwise, it contains the original error.</returns>
        public async ValueTask<ValueResult<TValue2, TError>> MapAsync<TValue2>(Func<TValue, ValueTask<TValue2>> mapAsyncFunc)
            => await (await resultAsync).MapAsync(mapAsyncFunc);

        /// <summary>
        /// Asynchronously transforms the error value of the result using the specified mapping function.
        /// </summary>
        /// <typeparam name="TError2">The type of the error value returned by the mapping function.</typeparam>
        /// <param name="mapFunc">A function to transform the error value if the result represents an error. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the error
        /// value mapped to the new type if an error was present; otherwise, the original successful value.</returns>
        public async ValueTask<ValueResult<TValue, TError2>> MapErrorAsync<TError2>(Func<TError, TError2> mapFunc)
            => (await resultAsync).MapError(mapFunc);

        /// <summary>
        /// Asynchronously transforms the error value of the result using the specified asynchronous mapping function,
        /// if the result represents an error.
        /// </summary>
        /// <remarks>If the result is successful, the mapping function is not invoked and the success
        /// value is preserved. If the result is an error, the mapping function is called to produce a new error value
        /// asynchronously.</remarks>
        /// <typeparam name="TError2">The type of the error value returned by the mapping function.</typeparam>
        /// <param name="mapAsyncFunc">A function that takes the current error value and returns a task representing the asynchronous mapping
        /// operation. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result with the original
        /// success value if present, or the mapped error value if the original result was an error.</returns>
        public async ValueTask<ValueResult<TValue, TError2>> MapErrorAsync<TError2>(Func<TError, ValueTask<TError2>> mapAsyncFunc)
            => await (await resultAsync).MapErrorAsync(mapAsyncFunc);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents a success or an error,
        /// returning a value of the specified type.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a unified, asynchronous
        /// manner. The appropriate delegate is invoked based on the result's state.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccess">A delegate to invoke if the result is successful. Receives the success value and returns a result of type
        /// TResult.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to invoke if the result represents an error. Receives the error value and returns a
        /// Task containing a result of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// onSuccess or onErrorAsync delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TValue, TResult> onSuccess, Func<TError, ValueTask<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccess, onErrorAsync);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents a success or an error.
        /// </summary>
        /// <remarks>If the result is successful, onSuccessAsync is invoked; otherwise, onError is
        /// invoked. The returned task completes when the selected delegate completes.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result is successful. The function receives the success value and
        /// returns a task that produces a result of type TResult.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function receives the error value and returns a
        /// result of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the value returned by either the
        /// onSuccessAsync or onError delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TValue, ValueTask<TResult>> onSuccessAsync, Func<TError, TResult> onError)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onError);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents a success or an error.
        /// </summary>
        /// <remarks>This method enables asynchronous pattern matching on the result, allowing different
        /// logic to be executed for success and error cases. Both delegates must be non-null and should not throw
        /// exceptions.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result is successful. The function receives the success value and
        /// returns a task that produces a result of type TResult.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result represents an error. The function receives the error value
        /// and returns a task that produces a result of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// onSuccessAsync or onErrorAsync delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TValue, ValueTask<TResult>> onSuccessAsync, Func<TError, ValueTask<TResult>> onErrorAsync)
            => await (await resultAsync).MatchAsync(onSuccessAsync, onErrorAsync);
    }
}
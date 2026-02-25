#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ValueResultOfTErrorExtensions
{
    extension<TError>(ValueResult<TError> result)
    {
        /// <summary>
        /// Invokes the specified function if the current result is successful, propagating the error otherwise.
        /// </summary>
        /// <remarks>This method enables chaining of operations that return results, allowing errors to be
        /// propagated without executing subsequent functions. It is commonly used in functional programming scenarios
        /// to compose operations that may fail.</remarks>
        /// <param name="bindFunc">A function to execute if the current result represents success. The function should return a new result to
        /// be used as the outcome.</param>
        /// <returns>A result returned by <paramref name="bindFunc"/> if the current result is successful; otherwise, a result
        /// containing the current error.</returns>
        public ValueResult<TError> Bind(Func<ValueResult<TError>> bindFunc)
            => result.IsSuccess
                ? bindFunc()
                : ValueResult<TError>.FromError(result.Error);

        /// <summary>
        /// Invokes the specified function if the current result is successful, propagating the error otherwise.
        /// </summary>
        /// <remarks>This method enables chaining of operations that return results, allowing error
        /// propagation without manual checks. If the current result is not successful, <paramref name="bindFunc"/> is
        /// not invoked.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the bind function and contained in the resulting result.</typeparam>
        /// <param name="bindFunc">A function to execute if the current result is successful. The function returns a result containing a value
        /// of type <typeparamref name="TValue2"/> or an error of type <typeparamref name="TError"/>.</param>
        /// <returns>A result containing the value returned by <paramref name="bindFunc"/> if the current result is successful;
        /// otherwise, a result containing the propagated error.</returns>
        public ValueResult<TValue2, TError> Bind<TValue2>(Func<ValueResult<TValue2, TError>> bindFunc)
            => result.IsSuccess
                ? bindFunc()
                : ValueResult<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Invokes the specified asynchronous function if the current result is successful, and returns its result;
        /// otherwise, returns a result containing the current error.
        /// </summary>
        /// <param name="bindAsyncFunc">A function that returns a task producing a result to bind to if the current result is successful. Cannot be
        /// null.</param>
        /// <returns>A task representing the asynchronous bind operation. If the current result is successful, the task completes
        /// with the result of the provided function; otherwise, it completes with a result containing the current
        /// error.</returns>
        public async ValueTask<ValueResult<TError>> BindAsync(Func<ValueTask<ValueResult<TError>>> bindAsyncFunc)
            => result.IsSuccess
                ? await bindAsyncFunc()
                : ValueResult<TError>.FromError(result.Error);

        /// <summary>
        /// Asynchronously applies the specified function to the result if it represents a successful outcome, returning
        /// a new result of the specified type.
        /// </summary>
        /// <remarks>This method enables chaining asynchronous operations that return results, propagating
        /// errors without invoking the binding function if the current result is unsuccessful.</remarks>
        /// <typeparam name="TValue2">The type of the value contained in the result returned by the asynchronous binding function.</typeparam>
        /// <param name="bindAsyncFunc">A function that returns a task producing a result of type <typeparamref name="TValue2"/> and <typeparamref
        /// name="TError"/>. This function is invoked if the current result is successful.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a result of type <typeparamref
        /// name="TValue2"/> and <typeparamref name="TError"/>. If the current result is successful, the returned result
        /// is produced by the binding function; otherwise, it contains the error from the current result.</returns>
        public async ValueTask<ValueResult<TValue2, TError>> BindAsync<TValue2>(Func<ValueTask<ValueResult<TValue2, TError>>> bindAsyncFunc)
            => result.IsSuccess
                ? await bindAsyncFunc()
                : ValueResult<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Transforms the successful result value to a new type using the specified mapping function, preserving the
        /// error if the result is unsuccessful.
        /// </summary>
        /// <remarks>This method enables chaining operations on result values without handling errors
        /// explicitly. If the result is unsuccessful, the mapping function is not called and the error is
        /// propagated.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the mapping function.</typeparam>
        /// <param name="mapFunc">A function that produces the new value when the result is successful. This function is not invoked if the
        /// result is an error.</param>
        /// <returns>A new result containing the mapped value if the original result is successful; otherwise, a result
        /// containing the original error.</returns>
        public ValueResult<TValue2, TError> Map<TValue2>(Func<TValue2> mapFunc)
            => result.IsSuccess
                ? ValueResult<TValue2, TError>.FromValue(mapFunc())
                : ValueResult<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Asynchronously transforms the successful result value to a new value using the specified asynchronous
        /// mapping function.
        /// </summary>
        /// <remarks>If the original result is not successful, the mapping function is not invoked and the
        /// error is propagated.</remarks>
        /// <typeparam name="TValue2">The type of the value returned by the mapping function and contained in the resulting result.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously produces a new value to replace the current successful result value. This
        /// function is invoked only if the original result is successful.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a new result object: if the
        /// original result is successful, the result contains the value returned by the mapping function; otherwise, it
        /// contains the original error.</returns>
        public async ValueTask<ValueResult<TValue2, TError>> MapAsync<TValue2>(Func<ValueTask<TValue2>> mapAsyncFunc)
            => result.IsSuccess
                ? ValueResult<TValue2, TError>.FromValue(await mapAsyncFunc())
                : ValueResult<TValue2, TError>.FromError(result.Error);

        /// <summary>
        /// Transforms the error value of the current result to a new error type using the specified mapping function.
        /// </summary>
        /// <remarks>If the current result is successful, the returned result will also be successful and
        /// the mapping function will not be invoked.</remarks>
        /// <typeparam name="TError2">The type to which the error value will be mapped.</typeparam>
        /// <param name="mapFunc">A function that takes the current error value and returns a new error value of type <typeparamref
        /// name="TError2"/>. This function is only called if the result represents an error.</param>
        /// <returns>A new <see cref="Result{TError2}"/> that is successful if the current result is successful; otherwise, a
        /// result containing the mapped error value.</returns>
        public ValueResult<TError2> MapError<TError2>(Func<TError, TError2> mapFunc)
            => result.IsSuccess
                ? ValueResult<TError2>.Success()
                : ValueResult<TError2>.FromError(mapFunc(result.Error));

        /// <summary>
        /// Asynchronously maps the error value of the result to a new error type using the specified asynchronous
        /// mapping function.
        /// </summary>
        /// <remarks>If the result is successful, the mapping function is not invoked and a successful
        /// result is returned. If the result is not successful, the mapping function is invoked to produce the new
        /// error value.</remarks>
        /// <typeparam name="TError2">The type of the error value to map to if the result is not successful.</typeparam>
        /// <param name="mapAsyncFunc">A function that takes the current error value and returns a task that produces the mapped error value.
        /// Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a new result containing the mapped
        /// error if the original result was not successful; otherwise, a successful result.</returns>
        public async ValueTask<ValueResult<TError2>> MapErrorAsync<TError2>(Func<TError, ValueTask<TError2>> mapAsyncFunc)
            => result.IsSuccess
                ? ValueResult<TError2>.Success()
                : ValueResult<TError2>.FromError(await mapAsyncFunc(result.Error));

        /// <summary>
        /// Invokes the specified delegate based on whether the result represents a success or an error, and returns the
        /// corresponding value.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a single expression,
        /// enabling functional-style result processing.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccess">A function to invoke and return its result if the result represents a success. Cannot be null.</param>
        /// <param name="onError">A function to invoke with the error value and return its result if the result represents an error. Cannot be
        /// null.</param>
        /// <returns>The value returned by either the <paramref name="onSuccess"/> or <paramref name="onError"/> delegate,
        /// depending on the result state.</returns>
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<TError, TResult> onError)
            => result.IsSuccess
                ? onSuccess()
                : onError(result.Error);

        /// <summary>
        /// Executes one of the provided delegates based on whether the result represents success or error, returning a
        /// value of the specified type asynchronously.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a result object, allowing
        /// for asynchronous error handling while keeping success handling synchronous. This is useful when error
        /// processing requires asynchronous operations, such as logging or remote calls.</remarks>
        /// <typeparam name="TResult">The type of the value to return from the delegate execution.</typeparam>
        /// <param name="onSuccess">A delegate to invoke if the result is successful. The delegate returns a value of type TResult.</param>
        /// <param name="onErrorAsync">An asynchronous delegate to invoke if the result represents an error. The delegate receives the error value
        /// and returns a Task that produces a value of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either the
        /// onSuccess or onErrorAsync delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> onSuccess, Func<TError, ValueTask<TResult>> onErrorAsync)
            => result.IsSuccess
                ? onSuccess()
                : await onErrorAsync(result.Error);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents success or error, and
        /// returns the corresponding value.
        /// </summary>
        /// <remarks>If the result is successful, the <paramref name="onSuccessAsync"/> delegate is
        /// invoked and awaited. If the result is an error, the <paramref name="onError"/> delegate is invoked with the
        /// error value. This method enables functional-style branching based on the result state.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result is successful. The function returns a task that produces
        /// the result value.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function receives the error value and returns
        /// the result value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the value returned by either the
        /// success or error delegate, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> onSuccessAsync, Func<TError, TResult> onError)
            => result.IsSuccess
                ? await onSuccessAsync()
                : onError(result.Error);

        /// <summary>
        /// Asynchronously invokes the specified delegate based on the result state, returning a value of the specified
        /// type.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a single asynchronous
        /// workflow. Only the delegate corresponding to the result state is invoked.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegate functions.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents success. The function returns a value of type
        /// <typeparamref name="TResult"/>.</param>
        /// <param name="onErrorAsync">A function to invoke asynchronously if the result represents an error. The function receives the error value
        /// and returns a value of type <typeparamref name="TResult"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by the
        /// invoked delegate.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> onSuccessAsync, Func<TError, ValueTask<TResult>> onErrorAsync)
            => result.IsSuccess
                ? await onSuccessAsync()
                : await onErrorAsync(result.Error);
    }
}
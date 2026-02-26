#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ResultDotNet;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ValueResultExtensions
{
    extension(ValueResult result)
    {
        /// <summary>
        /// Invokes the specified function if the current result represents a success; otherwise, returns the current
        /// result.
        /// </summary>
        /// <remarks>This method enables chaining of operations that return ValueResult instances,
        /// propagating failures without invoking subsequent functions.</remarks>
        /// <param name="bindFunc">A function to execute if the current result is successful. The function should return a new ValueResult.</param>
        /// <returns>The result of the bind function if the current result is successful; otherwise, the current result.</returns>
        public ValueResult Bind(Func<ValueResult> bindFunc)
            => result.IsSuccess
                ? bindFunc()
                : result;

        /// <summary>
        /// Invokes the specified asynchronous bind function if the current result is successful; otherwise, returns the
        /// current result.
        /// </summary>
        /// <param name="bindAsyncFunc">A function that returns a <see cref="ValueTask{ValueResult}"/> representing the asynchronous bind operation
        /// to perform if the current result is successful. Cannot be null.</param>
        /// <returns>A <see cref="ValueTask{ValueResult}"/> representing the result of the bind operation if the current result
        /// is successful; otherwise, the current result.</returns>
        public async ValueTask<ValueResult> BindAsync(Func<ValueTask<ValueResult>> bindAsyncFunc)
            => result.IsSuccess
                ? await bindAsyncFunc()
                : result;

        /// <summary>
        /// Maps the error value of the current result to a new error type using the specified mapping function.
        /// </summary>
        /// <remarks>If the current result is successful, the returned ValueResult<TError> will also
        /// indicate success and contain no error value. If the current result is an error, the mapping function is
        /// invoked to produce the new error value.</remarks>
        /// <typeparam name="TError">The type of the error value to map to.</typeparam>
        /// <param name="mapFunc">A function that provides the new error value if the result is not successful. Cannot be null.</param>
        /// <returns>A ValueResult<TError> that represents success if the current result is successful; otherwise, a result
        /// containing the mapped error value.</returns>
        public ValueResult<TError> MapError<TError>(Func<TError> mapFunc)
            => result.IsSuccess
                ? ValueResult<TError>.Success()
                : ValueResult<TError>.FromError(mapFunc());

        /// <summary>
        /// Asynchronously maps the error value of the current result to a new error type using the specified mapping
        /// function.
        /// </summary>
        /// <remarks>If the current result is successful, the mapping function is not invoked and a
        /// successful ValueResult is returned. This method is useful for transforming error values in asynchronous
        /// workflows.</remarks>
        /// <typeparam name="TError">The type of the error value to map to.</typeparam>
        /// <param name="mapAsyncFunc">A function that asynchronously produces the new error value. This function is invoked only if the current
        /// result represents an error.</param>
        /// <returns>A ValueResult containing the mapped error value if the current result is an error; otherwise, a successful
        /// ValueResult with no error.</returns>
        public async ValueTask<ValueResult<TError>> MapErrorAsync<TError>(Func<ValueTask<TError>> mapAsyncFunc)
            => result.IsSuccess
                ? ValueResult<TError>.Success()
                : ValueResult<TError>.FromError(await mapAsyncFunc());

        /// <summary>
        /// Invokes the specified delegate based on whether the result represents a success or an error, and returns the
        /// corresponding value.
        /// </summary>
        /// <remarks>This method enables functional-style handling of success and error cases by allowing
        /// the caller to provide separate logic for each outcome. Both delegates must be provided; otherwise, an
        /// exception may be thrown at runtime.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccess">A delegate to invoke and return its result if the underlying result is successful. Cannot be null.</param>
        /// <param name="onError">A delegate to invoke and return its result if the underlying result represents an error. Cannot be null.</param>
        /// <returns>The value returned by either the <paramref name="onSuccess"/> or <paramref name="onError"/> delegate,
        /// depending on the state of the result.</returns>
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<TResult> onError)
            => result.IsSuccess
                ? onSuccess()
                : onError();

        /// <summary>
        /// Executes one of the specified delegates based on whether the result represents a success or an error, and
        /// returns the corresponding value asynchronously.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a result object, allowing
        /// for asynchronous processing in the error case. The onSuccess delegate is executed synchronously, while
        /// onErrorAsync is awaited if the result is an error.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccess">The delegate to invoke if the result is successful. The return value of this delegate is returned by the
        /// method.</param>
        /// <param name="onErrorAsync">The asynchronous delegate to invoke if the result represents an error. The returned ValueTask provides the
        /// value to return.</param>
        /// <returns>A ValueTask that represents the result of invoking either the onSuccess or onErrorAsync delegate, depending
        /// on the state of the result.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> onSuccess, Func<ValueTask<TResult>> onErrorAsync)
            => result.IsSuccess
                ? onSuccess()
                : await onErrorAsync();

        /// <summary>
        /// Asynchronously invokes the specified delegate based on the result state and returns a value of the specified
        /// type.
        /// </summary>
        /// <remarks>If the result is successful, onSuccessAsync is awaited and its result is returned.
        /// Otherwise, onError is invoked synchronously. This method enables handling both success and error cases in a
        /// unified way.</remarks>
        /// <typeparam name="TResult">The type of the value to return from the delegate.</typeparam>
        /// <param name="onSuccessAsync">A function to invoke asynchronously if the result represents success. The function returns a value of type
        /// TResult.</param>
        /// <param name="onError">A function to invoke if the result represents an error. The function returns a value of type TResult.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by either
        /// onSuccessAsync or onError, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> onSuccessAsync, Func<TResult> onError)
            => result.IsSuccess
                ? await onSuccessAsync()
                : onError();

        /// <summary>
        /// Asynchronously invokes the specified delegate based on whether the result represents a success or an error,
        /// and returns the corresponding value.
        /// </summary>
        /// <remarks>Use this method to handle both success and error cases in a single asynchronous
        /// workflow. Only the delegate corresponding to the current result state will be invoked.</remarks>
        /// <typeparam name="TResult">The type of the value returned by the delegates.</typeparam>
        /// <param name="onSuccessAsync">A delegate to invoke asynchronously if the result is successful. The delegate returns a value of type
        /// TResult.</param>
        /// <param name="onErrorAsync">A delegate to invoke asynchronously if the result represents an error. The delegate returns a value of type
        /// TResult.</param>
        /// <returns>A ValueTask that represents the asynchronous operation. The task result is the value returned by either
        /// onSuccessAsync or onErrorAsync, depending on the result state.</returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> onSuccessAsync, Func<ValueTask<TResult>> onErrorAsync)
            => result.IsSuccess
                ? await onSuccessAsync()
                : await onErrorAsync();
    }
}
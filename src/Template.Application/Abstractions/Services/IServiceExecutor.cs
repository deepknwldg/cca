namespace Template.Application.Abstractions.Services;

public interface IServiceExecutor
{
    Task<TResponse> ExecuteAsync<TRequest, TResponse>(
        TRequest request,
        Func<TRequest, Task<TResponse>> action);
}

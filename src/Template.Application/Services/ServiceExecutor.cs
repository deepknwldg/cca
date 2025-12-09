using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Abstractions.Services;

namespace Template.Application.Services;

public class ServiceExecutor : IServiceExecutor
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceExecutor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(
        TRequest request,
        Func<TRequest, Task<TResponse>> action)
    {
        var validators = _serviceProvider
            .GetServices<IValidator<TRequest>>()
            .ToList();

        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var errors = validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(e => e != null)
                .ToList();

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }

        return await action(request);
    }
}

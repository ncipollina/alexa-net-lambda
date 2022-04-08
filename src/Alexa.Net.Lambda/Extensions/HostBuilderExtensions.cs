using Alexa.Net.Lambda.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alexa.Net.Lambda.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseHandler<THandler, TRequest, TResponse>(this IHostBuilder builder)
        where THandler : ILambdaHandler<TRequest, TResponse>
    {
        builder.UseHandler<TRequest, TResponse>(CreateDelegate<THandler, TRequest, TResponse>);
        return builder;
    }

    private static void UseHandler<TRequest, TResponse>(this IHostBuilder builder,
        Func<IServiceProvider, HandlerDelegate<TRequest, TResponse>> handlerFactory)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped(handlerFactory);
        });
    }

    private static HandlerDelegate<TRequest, TResponse>
        CreateDelegate<THandler, TRequest, TResponse>(IServiceProvider requestedServices)
        where THandler : ILambdaHandler<TRequest, TResponse>
    {
        var handler = ActivatorUtilities.CreateInstance<THandler>(requestedServices);

        return async (request, context) =>
        {
            var response = await handler.HandleAsync(request, context);
            return response;
        };
    }
}
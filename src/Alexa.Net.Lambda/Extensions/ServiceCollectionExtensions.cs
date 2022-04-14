using Alexa.Net.Lambda.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Alexa.Net.Lambda.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSkillContextAccessor(this IServiceCollection services)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));
        services.TryAddSingleton<ISkillContextAccessor, SkillContextAccessor>();
        return services;
    }
}
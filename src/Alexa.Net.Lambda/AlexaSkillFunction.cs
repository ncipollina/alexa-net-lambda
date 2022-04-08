using Alexa.Net.Lambda.Abstractions;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alexa.Net.Lambda;

public abstract class AlexaSkillFunction : AlexaSkillFunction<SkillRequest, SkillResponse>
{
}

public abstract class AlexaSkillFunction<TRequest, TResponse>
{
    private IServiceProvider _serviceProvider;

    protected AlexaSkillFunction()
    {
        Start();
    }

    protected virtual IHostBuilder CreateHostBuilder()
    {
        var builder = Host.CreateDefaultBuilder().ConfigureLogging((context, logging) =>
        {
            logging.AddJsonConsole();
            logging.AddDebug();
        });
        Init(builder);
        return builder;
    }
    
    protected virtual void Init(IHostBuilder builder){ }

    protected void Start()
    {
        var builder = CreateHostBuilder();

        var host = builder.Build();
        
        host.Start();
        _serviceProvider = host.Services;
    }
    
    // Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
    public virtual async Task<TResponse> FunctionHandlerAsync(TRequest request, ILambdaContext lambdaContext)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        var provider = serviceScope.ServiceProvider;
        var handlerAsync = provider.GetRequiredService<HandlerDelegate<TRequest, TResponse>>();

        return await handlerAsync(request, lambdaContext);
    }

}
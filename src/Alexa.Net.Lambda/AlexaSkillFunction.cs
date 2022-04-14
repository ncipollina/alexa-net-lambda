using Alexa.Net.Lambda.Abstractions;
using Alexa.Net.Lambda.Context;
using Alexa.Net.Lambda.Extensions;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alexa.Net.Lambda;

public abstract class AlexaSkillFunction
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
        builder.ConfigureServices(services =>
        {
            services.AddSkillContextAccessor();
            services.AddSingleton<ISkillContextFactory, DefaultSkillContextFactory>();
        });
        var host = builder.Build();
        
        host.Start();
        _serviceProvider = host.Services;
    }

    protected void CreateContext(SkillRequest request)
    {
        var factory = _serviceProvider.GetRequiredService<ISkillContextFactory>();
        factory.Create(request);
    }
    
    // Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
    public virtual async Task<SkillResponse> FunctionHandlerAsync(SkillRequest request, ILambdaContext lambdaContext)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        var provider = serviceScope.ServiceProvider;
        CreateContext(request);
        var handlerAsync = provider.GetRequiredService<HandlerDelegate<SkillRequest, SkillResponse>>();

        return await handlerAsync(request, lambdaContext);
    }

}
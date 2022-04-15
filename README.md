# Alexa Skills Base Lambda Function

## Package Version
| Build Status                                                                                                                                                                                    | Nuget |
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------| ----- |
| [![.Net Build and Package](https://github.com/ncipollina/alexa-net-lambda/actions/workflows/build.yaml/badge.svg)](https://github.com/ncipollina/alexa-net-lambda/actions/workflows/build.yaml) | [![NuGet Badge](https://buildstats.info/nuget/alexa.net.lambda)](https://www.nuget.org/packages/Alexa.Net.Lambda/) |

Simple package to enable Alexa Skill development in .Net with Dependency Injection.

# Setup

In order to get started with this package, you will need to install the package first.

```powershell
Install-Package Alexa.Net.Lambda
```

## Handler

A custom `ILambdaHandler` will need to be implemented to handle the actual request from Alexa. The handler should look something like the following. In the code below, `IInputService` is injected into the handler via Dependency Injection.

```c#
public class RequestHandler : ILambdaHandler<SkillRequest, SkillResponse>
{
    private readonly IInputService _inputService;
 
    public RequestHandler(IInputService inputService, ITransientService transientService, IScopedService scopedService)
    {
        _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
    }

    public async Task<SkillResponse> HandleAsync(SkillRequest request, ILambdaContext context)
    {
        return await _inputService.ProcessInput(request);
    }
}
```

## DI Setup

In order to register items with the DI framework, the `AlexaSkillFunction` should be implemented. Override the `Init` method to configure your services and any other startup type of items, such as logging etc.

```c#

public class Function : AlexaSkillFunction
{
    protected override void Init(IHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
            {
                logging.AddJsonConsole();
                logging.AddDebug();
            })
            .UseHandler<RequestHandler, SkillRequest, SkillResponse>()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IInputService, InputService>();
            });
    }
}

```

In the above code the Handler is registered via the `UseHandler` extension method.
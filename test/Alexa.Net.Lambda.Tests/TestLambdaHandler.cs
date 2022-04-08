using System.Threading.Tasks;
using Alexa.Net.Lambda.Abstractions;
using Amazon.Lambda.Core;

namespace Alexa.Net.Lambda.Tests;

public class TestLambdaHandler : ILambdaHandler<string,string>
{
    public Task<string> HandleAsync(string request, ILambdaContext context)
    {
        return Task.FromResult(request.ToUpper());
    }
}
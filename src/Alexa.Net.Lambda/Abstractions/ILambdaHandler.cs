using Amazon.Lambda.Core;

namespace Alexa.Net.Lambda.Abstractions;

public interface ILambdaHandler<in TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, ILambdaContext context);
}
using Amazon.Lambda.Core;

namespace Alexa.Net.Lambda.Abstractions;

public delegate Task<TResponse> HandlerDelegate<in TRequest, TResponse>(TRequest request, ILambdaContext context);
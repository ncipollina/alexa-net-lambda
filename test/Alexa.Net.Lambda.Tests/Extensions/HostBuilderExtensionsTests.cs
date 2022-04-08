using System.Threading.Tasks;
using Alexa.Net.Lambda.Abstractions;
using Alexa.Net.Lambda.Extensions;
using Amazon.Lambda.Core;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace Alexa.Net.Lambda.Tests.Extensions;

public class HostBuilderExtensionsTests
{
    [Fact]
    public void CreateDelegate_WithService_CreatesDelegate()
    {
        var host = Host.CreateDefaultBuilder()
            .UseHandler<TestLambdaHandler, string, string>()
            .Build();

        var handlerDelegate = host.Services.GetRequiredService<HandlerDelegate<string, string>>();

        handlerDelegate.Should().NotBeNull();
        handlerDelegate.Should().BeOfType<HandlerDelegate<string, string>>();
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("Hello")]
    [InlineData("HeLlO")]
    public async Task CreateDelegate_WithService_HandlerReturnsValue(string input)
    {
        var host = Host.CreateDefaultBuilder()
            .UseHandler<TestLambdaHandler, string, string>()
            .Build();

        var handlerDelegate = host.Services.GetRequiredService<HandlerDelegate<string, string>>();

        var result = await handlerDelegate(input, Mock.Of<ILambdaContext>());

        result.Should().BeUpperCased(input);
    }
}
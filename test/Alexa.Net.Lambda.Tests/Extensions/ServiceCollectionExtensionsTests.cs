using System;
using Alexa.Net.Lambda.Context;
using Alexa.Net.Lambda.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alexa.Net.Lambda.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddSkillContextAccessor_NullServices_ThrowsException()
    {
        var s = (IServiceCollection)null!;

        Action act = () => s.AddSkillContextAccessor();

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddSkillContextAccessor_WithServicesCollection_ReturnsSkillContextFactory()
    {
        var s = new ServiceCollection();

        s.AddSkillContextAccessor();

        var serviceProvider = s.BuildServiceProvider();

        var skillContextFactory = serviceProvider.GetService<ISkillContextFactory>();

        skillContextFactory.Should().NotBeNull();
        skillContextFactory.Should().BeAssignableTo<ISkillContextFactory>();
    }
    
    [Fact]
    public void AddSkillContextAccessor_WithServicesCollection_ReturnsSkillContextAccessor()
    {
        var s = new ServiceCollection();

        s.AddSkillContextAccessor();

        var serviceProvider = s.BuildServiceProvider();

        var skillContextFactory = serviceProvider.GetService<ISkillContextAccessor>();

        skillContextFactory.Should().NotBeNull();
        skillContextFactory.Should().BeAssignableTo<ISkillContextAccessor>();
    }
}
using System;
using Alexa.Net.Lambda.Context;
using Alexa.NET.Request;
using FluentAssertions;
using Moq;
using Xunit;

namespace Alexa.Net.Lambda.Tests.Context;

public class DefaultSkillContextFactoryTests
{
    [Fact]
    public void Create_WithSkillRequest_ReturnsContext()
    {
        var request = new SkillRequest();
        var skillContextFactory = GetDefaultSkillContextFactory();

        var context = skillContextFactory.Create(request);

        context.Should().NotBeNull();
        context.Should().BeAssignableTo<SkillContext>();
        context.Request.Should().Be(request);
    }

    [Fact]
    public void Create_WithNullAccessor_ReturnsContext()
    {
        var request = new SkillRequest();
        var skillContextFactory = new DefaultSkillContextFactory(null);

        var context = skillContextFactory.Create(request);
        
        context.Should().NotBeNull();
        context.Should().BeAssignableTo<SkillContext>();
        context.Request.Should().Be(request);
    }

    private DefaultSkillContextFactory GetDefaultSkillContextFactory()
    {
        var mockSkillContextAccessor = CreateDefaultSkillContextAccessor();
        var skillContextFactory = new DefaultSkillContextFactory(mockSkillContextAccessor);
        return skillContextFactory;
    }

    private static ISkillContextAccessor CreateDefaultSkillContextAccessor()
    {
        var mockSkillContextAccessor = new Mock<ISkillContextAccessor>();
        return mockSkillContextAccessor.Object;
    }
}
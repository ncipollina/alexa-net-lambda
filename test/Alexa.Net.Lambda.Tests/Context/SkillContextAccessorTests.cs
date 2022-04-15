using Alexa.Net.Lambda.Context;
using Alexa.NET.Request;
using FluentAssertions;
using Xunit;

namespace Alexa.Net.Lambda.Tests.Context;

public class SkillContextAccessorTests
{
    [Fact]
    public void get_SkillContext_NotSet_ReturnsNull()
    {
        var skilContextAccessor = new SkillContextAccessor();

        var context = skilContextAccessor.SkillContext;

        context.Should().BeNull();
    }

    [Fact]
    public void get_SkillContext_WithContext_ReturnsContext()
    {
        var skillContextAccessor = new SkillContextAccessor();
        var context = new DefaultSkillContext(new SkillRequest());
        skillContextAccessor.SkillContext = context;

        var contextFromAccessor = skillContextAccessor.SkillContext;

        contextFromAccessor.Should().Be(context);
    }

    [Fact]
    public void set_SkillContext_WithNull_ReturnsNull()
    {
        var skillContextAccessor = new SkillContextAccessor();

        skillContextAccessor.SkillContext = null;
        var context = skillContextAccessor.SkillContext;

        context.Should().BeNull();
    }
}
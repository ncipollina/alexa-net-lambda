using Alexa.NET.Request;

namespace Alexa.Net.Lambda.Context;

public sealed class DefaultSkillContext : SkillContext
{
    private readonly SkillRequest _request;
    
    public DefaultSkillContext(SkillRequest request) => _request = request;

    public override SkillRequest Request => _request;
}
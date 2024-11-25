using Alexa.NET.Request;

namespace Alexa.Net.Lambda.Context;

public abstract class SkillContext
{
    public abstract SkillRequest Request { get; }
    
}
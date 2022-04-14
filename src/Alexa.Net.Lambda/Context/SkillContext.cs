using Alexa.NET.Request;
using Alexa.NET.Response;

namespace Alexa.Net.Lambda.Context;

public abstract class SkillContext
{
    public abstract SkillRequest Request { get; }
    
}
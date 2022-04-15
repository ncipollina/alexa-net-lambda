using Alexa.NET.Request;

namespace Alexa.Net.Lambda.Context;

public interface ISkillContextFactory
{
    SkillContext Create(SkillRequest request);

    void Dispose(SkillContext skillContext);
}
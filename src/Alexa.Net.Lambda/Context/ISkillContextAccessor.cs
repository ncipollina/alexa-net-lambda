namespace Alexa.Net.Lambda.Context;

public interface ISkillContextAccessor
{
    SkillContext? SkillContext { get; set; }
}
namespace Alexa.Net.Lambda.Context;

public class SkillContextAccessor : ISkillContextAccessor
{
    private static readonly AsyncLocal<SkillContextHolder> _skillContextCurrent = new AsyncLocal<SkillContextHolder>();

    public SkillContext? SkillContext
    {
        get
        {
            return _skillContextCurrent.Value?.Context;
        }
        set
        {
            var holder = _skillContextCurrent.Value;
            if (holder is not null)
            {
                holder.Context = null;
            }

            if (value is not null)
            {
                _skillContextCurrent.Value = new SkillContextHolder { Context = value };
            }
        }
    }
        
    private class SkillContextHolder
    {
        public SkillContext? Context;
    }
}
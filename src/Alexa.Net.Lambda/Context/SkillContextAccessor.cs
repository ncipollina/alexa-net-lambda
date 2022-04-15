﻿namespace Alexa.Net.Lambda.Context;

public class SkillContextAccessor : ISkillContextAccessor
{
    private static readonly AsyncLocal<SkillContextHolder> SkillContextCurrent = new();

    public SkillContext? SkillContext
    {
        get
        {
            return SkillContextCurrent.Value?.Context;
        }
        set
        {
            var holder = SkillContextCurrent.Value;
            if (holder is not null)
            {
                holder.Context = null;
            }

            if (value is not null)
            {
                SkillContextCurrent.Value = new SkillContextHolder { Context = value };
            }
        }
    }
        
    private class SkillContextHolder
    {
        public SkillContext? Context;
    }
}
﻿using Alexa.NET.Request;

namespace Alexa.Net.Lambda.Context;

public class DefaultSkillContextFactory : ISkillContextFactory
{
    private readonly ISkillContextAccessor? _skillContextAccessor;

    public DefaultSkillContextFactory(ISkillContextAccessor skillContextAccessor)
    {
        _skillContextAccessor = skillContextAccessor;
    }
    public SkillContext Create(SkillRequest request)
    {
        var skillContext = new DefaultSkillContext(request);
        Initialize(skillContext);
        return skillContext;
    }

    public void Dispose(SkillContext skillContext)
    {
        if (_skillContextAccessor is not null)
        {
            _skillContextAccessor.SkillContext = null;
        }
    }

    private DefaultSkillContext Initialize(DefaultSkillContext skillContext)
    {
        if (_skillContextAccessor is not null)
        {
            _skillContextAccessor.SkillContext = skillContext;
        }

        return skillContext;
    }
}
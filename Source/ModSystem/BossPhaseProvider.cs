using System;

namespace PromisedEigong.ModSystem;

public class BossPhaseProvider
{
    public event Action<int>? OnPhaseChangePreAnimation;
    public event Action<int>? OnPhaseChangePostAnimation;

    public int CurrentPreAnimationPhase { get; private set; } = -1;
    public int CurrentPostAnimationPhase { get; private set; }
    
    MonsterBase monster;
    
    public void Setup (MonsterBase monster)
    {
        this.monster = monster;
    }
    
    public void HandleUpdateStep ()
    {
        if (monster == null)
            return;
        
        CheckPreAnimationChange();
        CheckPostAnimationChange();
    }

    void CheckPreAnimationChange ()
    {
        if (monster.PhaseIndex == CurrentPreAnimationPhase)
            return;

        if (monster.currentMonsterState != monster.GetState(MonsterBase.States.BossAngry) &&
            monster.currentMonsterState != monster.GetState(MonsterBase.States.FooStunEnter))
            return;
        
        CurrentPreAnimationPhase = monster.PhaseIndex;
        OnPhaseChangePreAnimation?.Invoke(CurrentPreAnimationPhase);
    }

    void CheckPostAnimationChange ()
    {
        if (monster.PhaseIndex == CurrentPostAnimationPhase)
            return;
        
        CurrentPostAnimationPhase = monster.PhaseIndex;
        OnPhaseChangePostAnimation?.Invoke(CurrentPostAnimationPhase);
    }
}
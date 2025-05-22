using NineSolsAPI;

namespace PromisedEigong.SpeedChangers;
#nullable disable

public class SpeedChangerManager
{
    BossGeneralState[] allBossStates;
    BaseSpeedChanger[] speedChangers;
    int currentPhase;
    
    public void SetBossStates (BossGeneralState[] allBossStates)
    {
        this.allBossStates = allBossStates;
    }

    public void SetPhase (int phase)
    {
        currentPhase = phase;
    }
    
    public void SetupSpeedChangers (params BaseSpeedChanger[] speedChangers)
    {
        this.speedChangers = speedChangers;
    }

    public void ProcessSpeeds ()
    {
        foreach (var bossState in allBossStates)
        {
            bool foundAlteredSpeed = false;
            bossState.OverideAnimationSpeed = true;

            foreach (var speedChanger in speedChangers)
            {
                if (!speedChanger.IsSpecifiedAttack(bossState.name)) 
                    continue;

                if (speedChanger.ShouldChangeClearEnterSpeedValue)
                    bossState.EnterClearSpeed = speedChanger.ClearEnterSpeedValue;
                
                if (speedChanger.ShouldChangeClearExitSpeedValue)
                    bossState.ClearSpeedWhenExit = speedChanger.ClearExitSpeedValue;
                
                var newSpeed = speedChanger.GetSpeed(currentPhase);
                bossState.AnimationSpeed = newSpeed;
                foundAlteredSpeed = true;
                break;
            }
            
            if (!foundAlteredSpeed)
                bossState.AnimationSpeed = 1;
        }
    }
}
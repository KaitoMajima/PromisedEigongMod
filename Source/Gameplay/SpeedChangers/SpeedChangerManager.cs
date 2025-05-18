namespace PromisedEigong.SpeedChangers;
#nullable disable

public class SpeedChangerManager
{
    BossGeneralState[] allBossStates;
    
    public void SetBossStates (BossGeneralState[] allBossStates)
    {
        this.allBossStates = allBossStates;
    }
    
    public void ChangeSpeedValues (params BaseSpeedChanger[] speedChangers)
    {
        foreach (var bossState in allBossStates)
        {
            bool foundAlteredSpeed = false;
            bossState.OverideAnimationSpeed = true;

            foreach (var speedChanger in speedChangers)
            {
                if (!speedChanger.IsSpecifiedAttack(bossState.name)) 
                    continue;
                
                var newSpeed = speedChanger.GetSpeed(bossState);
                bossState.AnimationSpeed = newSpeed;
                foundAlteredSpeed = true;
                break;
            }
            
            if (!foundAlteredSpeed)
                bossState.AnimationSpeed = 1;
        }
    }
}
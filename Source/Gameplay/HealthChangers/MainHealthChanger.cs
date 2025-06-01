namespace PromisedEigong.Gameplay.HealthChangers;

using HarmonyLib;
using static HarmonyLib.AccessTools;

public class MainHealthChanger
{
    FieldRef<MonsterStat, float> HealthFieldRef => FieldRefAccess<MonsterStat, float>("BaseHealthValue");

    public void ChangeBaseHealthStat (MonsterBase monsterTarget, float healthValue)
    {
        monsterTarget.monsterStat.BossMemoryHealthScale = 1;
        monsterTarget.monsterStat.BossMemoryAttackScale = 1;
        HealthFieldRef.Invoke(monsterTarget.monsterStat) = healthValue;
    }

    public void ChangeHealthInPhase1 (MonsterBase monsterTarget, float healthValue)
    {
        monsterTarget.postureSystem.CurrentHealthValue = healthValue;
    }

    public void ChangeHealthInRemainingPhases (
        MonsterBase monsterTarget, 
        int phaseIndex, 
        float healthValue,
        float baseHealth
    )
    {
        switch (phaseIndex)
        {
            case 1:
                monsterTarget.monsterStat.Phase2HealthRatio = healthValue / baseHealth;
                break;
            case 2:
                monsterTarget.monsterStat.Phase3HealthRatio = healthValue / baseHealth;
                break;
        }
    }
}
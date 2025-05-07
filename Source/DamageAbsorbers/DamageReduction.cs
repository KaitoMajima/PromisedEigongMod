namespace PromisedEigong;

using static PromisedEigongModGlobalSettings.EigongDamageReduction;

public static class DamageReduction
{
    public static float GetReducedPostureDamage ()
    {
        float postureDamageReduction = EIGONG_POSTURE_DAMAGE_DR;

        var player = Player.i;
        
        bool isPlayerUsingDivineJade = player.mainAbilities.MultipleCounterJade.IsActivated;
        bool isPlayerCurrentlyCountering = player.IsCurrentState(PlayerStateType.ParryCounterDefense);
        
        if (isPlayerUsingDivineJade && isPlayerCurrentlyCountering)
            postureDamageReduction += EIGONG_COUNTER_JADE_DAMAGE_DR;

        return CalculateDamageReduction(postureDamageReduction);
    }

    public static float GetReducedDirectDamage (EffectDetailType detailType)
    {
        var player = Player.i;
        float directDamageReduction;
        
        switch (detailType)
        {
            case EffectDetailType.Undefined:
                directDamageReduction = CalculateDamageReduction(EIGONG_NORMAL_AND_ELECTRIC_ARROW_DR);
                break;
            case EffectDetailType.PlayerArrow:
                directDamageReduction = CalculateDamageReduction(EIGONG_SPIKED_ARROW_DR);
                break;
            case EffectDetailType.PlayerAirArrow:
                directDamageReduction = CalculateDamageReduction(EIGONG_SPIKED_ARROW_DR);
                break;
            case EffectDetailType.PlayerChargeAttack:
                directDamageReduction = CalculateDamageReduction(EIGONG_CHARGE_ATTACK_DR);
                break;
            case EffectDetailType.PlayerThirdAttack:
                bool isPlayerUsingQiBladeJade = player.mainAbilities.ThirdAttackPowerJade.IsActivated;
                directDamageReduction =
                    CalculateDamageReduction(isPlayerUsingQiBladeJade
                        ? EIGONG_THIRD_ATTACK_JADE_DR
                        : EIGONG_DEFAULT_DR
                    );
                break;
            case EffectDetailType.FooHit:
                directDamageReduction = CalculateDamageReduction(EIGONG_FOO_DR);
                break;
            case EffectDetailType.FooExplode:
                directDamageReduction = CalculateDamageReduction(EIGONG_FOO_DR);
                break;
            default:
                directDamageReduction = CalculateDamageReduction(EIGONG_DEFAULT_DR);
                break;
        }

        return directDamageReduction;
    }
    
    static float CalculateDamageReduction (float reductionRate) => 1 - reductionRate;
}
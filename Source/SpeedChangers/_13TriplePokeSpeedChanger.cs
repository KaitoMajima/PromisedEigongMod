namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _13TriplePokeSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK13_TRIPLE_POKE;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK13_TRIPLE_POKE_SPEED;
}
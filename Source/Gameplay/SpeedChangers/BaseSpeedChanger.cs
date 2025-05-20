namespace PromisedEigong.SpeedChangers;

public abstract class BaseSpeedChanger
{
    public abstract bool IsSpecifiedAttack (string attack);
    public abstract float GetSpeed (int phase);
}
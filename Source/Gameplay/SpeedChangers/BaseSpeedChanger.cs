namespace PromisedEigong.SpeedChangers;

public abstract class BaseSpeedChanger
{
    public virtual bool ShouldChangeClearEnterSpeedValue { get; }
    public virtual bool ClearEnterSpeedValue { get; }
    
    public virtual bool ShouldChangeClearExitSpeedValue { get; }
    public virtual bool ClearExitSpeedValue { get; }
    
    public abstract bool IsSpecifiedAttack (string attack);
    public abstract float GetSpeed (int phase);
}
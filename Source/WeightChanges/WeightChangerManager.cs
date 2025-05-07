using PromisedEigong.WeightChanges;

public class WeightChangerManager
{
    public void ChangeWeights (params BaseWeightChanger[] weightChangers)
    {
        foreach (var weightChanger in weightChangers)
            weightChanger.ChangeAttackWeight();
    }
}
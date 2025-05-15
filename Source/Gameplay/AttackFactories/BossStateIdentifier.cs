using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

public class BossStateIdentifier : MonoBehaviour
{
    public string IdName { get; private set; }

    public void Setup (string name)
    {
        IdName = name;
    }
}
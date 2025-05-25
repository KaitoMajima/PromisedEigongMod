using UnityEngine;

namespace PromisedEigong.LevelChangers;

public class PlayerWrapper : MonoBehaviour
{
    public static bool IsPlayerJustAirborne => isPlayerJustAirborne;
    public static bool IsPlayerJustDashing => isPlayerJustDashing;

    static bool isPlayerJustAirborne;
    static bool isPlayerJustDashing;
    
    float airborneCoyoteTime = 0.2f;
    float airborneTimer;
    float dashingCoyoteTime = 0.2f;
    float dashingTimer;
    
    Player player;
    
    void Awake ()
    {
        player = GetComponent<Player>();
    }

    void Update ()
    {
        ApplyCoyoteTime(player.IsAirBorne, airborneCoyoteTime, ref isPlayerJustAirborne, ref airborneTimer);
        ApplyCoyoteTime(player.IsDodgeAttack, dashingCoyoteTime, ref isPlayerJustDashing, ref dashingTimer);
    }

    void ApplyCoyoteTime (bool originalCheck, float coyoteTimeMax, ref bool changedCheck, ref float timer)
    {
        if (originalCheck)
        {
            changedCheck = true;
            timer = coyoteTimeMax;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                changedCheck = false;
        }
    }
}
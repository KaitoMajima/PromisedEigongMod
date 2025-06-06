﻿namespace PromisedEigong.PreloadObjectHandlers;
#nullable disable

using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongBackground;

public class BigBadHandler : MonoBehaviour
{
    Transform playerTransform;
    Transform bigBadHead;
    Transform bigBadHair;
    
    bool waitedForFirstUpdateFrame;
    bool waitedForFirstBigBadActivationFrame;

    void Awake ()
    {
        transform.localPosition = BIG_BAD_HEAD_POSITION;
    }

    void Update ()
    {
        ProcessBigBadRotation();
        
        if (waitedForFirstUpdateFrame)
            return;

        GetBigBadHeadAndHair();
        AdjustHairSize();
        DuplicateNeckvines();
        DeactivateStaticObjs();
        ModifyMeatball();
        DisableMeatball();
        IncreaseSizeAndKillAnimator();
        waitedForFirstUpdateFrame = true;
    }

    void ProcessBigBadRotation ()
    {
        if (!gameObject.activeInHierarchy)
            return;
        
        if (bigBadHead == null || bigBadHair == null || playerTransform == null)
            return;
        
        var direction = transform.position - playerTransform.position;

        var minTolerance = 0.001f;
        
        if (direction.sqrMagnitude < minTolerance)
            return;

        Quaternion currentHeadRotation = bigBadHead.rotation;
        Quaternion currentHairRotation = bigBadHair.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        bigBadHead.rotation = waitedForFirstBigBadActivationFrame ? Quaternion.Slerp(currentHeadRotation, targetRotation, Time.deltaTime * BIG_BAD_HEAD_SPEED) : targetRotation;
        bigBadHair.rotation = waitedForFirstBigBadActivationFrame ? Quaternion.Slerp(currentHairRotation, targetRotation, Time.deltaTime * BIG_BAD_HEAD_SPEED) : targetRotation;
        waitedForFirstBigBadActivationFrame = true;
    }
    
    void GetBigBadHeadAndHair ()
    {
        playerTransform = Player.i.gameObject.transform;
        bigBadHead = GameObject.Find(BIG_BAD_HEAD).transform;
        bigBadHair = GameObject.Find(BIG_BAD_HAIR).transform;
    }
    
    void AdjustHairSize ()
    {
        bigBadHair.localScale = BIG_BAD_HAIR_SCALE;
    }

    void DuplicateNeckvines ()
    {
        var neckvines = GameObject.Find(BIG_BAD_NECKVINES);
        var neckvinesParent = GameObject.Find(BIG_BAD_NECKVINES_PARENT);
        var newNeckvines = Instantiate(neckvines, neckvinesParent.transform);
        newNeckvines.transform.SetLocalPositionAndRotation(neckvines.transform.localPosition, Quaternion.Euler(BIG_BAD_NECKVINES_ROTATION));
        neckvines.transform.localPosition += BIG_BAD_NECK_VINES_OFFSET;
        newNeckvines.transform.localPosition += BIG_BAD_NECK_VINES_OFFSET;
    }
    
    void DeactivateStaticObjs ()
    {
        var staticObjs = GameObject.Find(BIG_BAD_STATIC_OBJS);
        staticObjs.SetActive(false);
    }

    void DisableMeatball ()
    {
        var meatball = GameObject.Find(BIG_BAD_MEATBALL);
        meatball.SetActive(false);
    }
    
    void ModifyMeatball ()
    {
        var meatball = GameObject.Find(BIG_BAD_MEATBALL_2);
        var meatballParticles = meatball.GetComponent<ParticleSystem>();
        var meatballShape = meatballParticles.shape;
        meatballShape.position = BIG_BAD_MEATBALL_2_SHAPE_POSITION;
        meatballShape.scale = BIG_BAD_MEATBALL_2_SHAPE_SCALE;
    }
    
    void IncreaseSizeAndKillAnimator ()
    {
        var bigBadAnim = GameObject.Find(BIG_BAD_ANIMATION);
        bigBadAnim.GetComponent<Animator>().enabled = false;

        bigBadHead.transform.localScale = BIG_BAD_HEAD_SCALE;
    }
}
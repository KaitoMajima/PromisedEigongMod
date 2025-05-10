using System;
using NineSolsAPI;

namespace PromisedEigong.PreloadObjectHandlers;

using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongBackground;

public class BigBadHandler : MonoBehaviour
{
    bool waitedForFirstUpdateFrame;

    void Update ()
    {
        if (waitedForFirstUpdateFrame)
            return;
        
        DuplicateNeckvines();
        DuplicateStaticObjs();
        // DuplicateMeatball();
        ModifyMeatball();
        waitedForFirstUpdateFrame = true;
    }

    void DuplicateNeckvines ()
    {
        var neckvines = GameObject.Find(BIG_BAD_NECKVINES);
        var neckvinesParent = GameObject.Find(BIG_BAD_NECKVINES_PARENT);
        var newNeckvines = Instantiate(neckvines, neckvinesParent.transform);
        newNeckvines.transform.SetLocalPositionAndRotation(neckvines.transform.localPosition, Quaternion.Euler(BIG_BAD_NECKVINES_ROTATION));
    }
    
    void DuplicateStaticObjs ()
    {
        var staticObjs = GameObject.Find(BIG_BAD_STATIC_OBJS);
        var staticObjsParent = GameObject.Find(BIG_BAD_STATIC_OBJS_PARENT);
        var newStaticObjs  = Instantiate(staticObjs, staticObjsParent.transform);
        newStaticObjs.transform.SetLocalPositionAndRotation(BIG_BAD_STATIC_OBJS_POSITION, Quaternion.Euler(BIG_BAD_STATIC_OBJS_ROTATION));
        staticObjs.transform.localScale = new Vector3(0.92f, 0.92f, 0.92f);
        newStaticObjs.transform.localScale = new Vector3(0.92f, 0.92f, 0.92f);
    }

    void DuplicateMeatball ()
    {
        var meatball = GameObject.Find(BIG_BAD_MEATBALL_1);
        var meatballParent = GameObject.Find(BIG_BAD_MEATBALL_PARENT);
        var newMeatball  = Instantiate(meatball, meatballParent.transform);
        newMeatball.transform.SetLocalPositionAndRotation(meatball.transform.localPosition, Quaternion.Euler(BIG_BAD_MEATBALL_ROTATION));
    }
    
    void ModifyMeatball ()
    {
        var meatball = GameObject.Find(BIG_BAD_MEATBALL_2);
        var meatballParticles = meatball.GetComponent<ParticleSystem>();
        var meatballShape = meatballParticles.shape;
        meatballShape.position = BIG_BAD_MEATBALL_2_SHAPE_POSITION;
        meatballShape.scale = BIG_BAD_MEATBALL_2_SHAPE_SCALE;
    }
}
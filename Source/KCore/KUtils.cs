namespace PromisedEigong.Core;

using System;
using System.Linq;
using UnityEngine;

public class KUtils
{
    internal static GameObject? GetGameObjectFromArray (GameObject[] objects, string objName)
    {
        string n = String.Empty;
        int length = objName.IndexOf('/');
        string rootName;
        switch (length)
        {
            case -1:
                rootName = objName;
                break;
            case 0:
                throw new ArgumentException("Invalid GameObject path");
            default:
                if (length != objName.Length - 1)
                {
                    rootName = objName.Substring(0, length);
                    string str = objName;
                    int startIndex = length + 1;
                    n = str.Substring(startIndex, str.Length - startIndex);
                    break;
                }
                goto case 0;
        }
        GameObject? gameObjectFromArray = objects.FirstOrDefault((Func<GameObject, bool>) (o => o.name == rootName));
        if (gameObjectFromArray == null)
            return null;
        if (n == string.Empty)
            return gameObjectFromArray;
        Transform transform = gameObjectFromArray.transform.Find(n);
        return transform ? transform.gameObject : null;
    }
}
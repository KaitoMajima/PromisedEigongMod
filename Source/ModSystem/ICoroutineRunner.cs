using System.Collections;
using UnityEngine;

namespace PromisedEigong.ModSystem;

public interface ICoroutineRunner
{
    Coroutine StartCoroutine (IEnumerator routine);
    void StopCoroutine (IEnumerator routine);
}
using System.Collections;
using PromisedEigong.ModSystem;

namespace PromisedEigong.Effects.MusicPlayers;
using UnityEngine;

public class GameMusicPlayer
{
    static IEnumerator currentRoutine;
    
    public static AmbienceSource GetAmbienceSourceAtPath (string path) 
        => GameObject.Find(path).GetComponent<AmbienceSource>();

    public static void ChangeMusic (
        ICoroutineRunner coroutineRunner,
        AmbienceSource ambienceSource, 
        string musicName, 
        float delay)
    {
        ambienceSource.ambPair.sound = musicName;
        currentRoutine = PlayOST(ambienceSource, delay);
        coroutineRunner.StartCoroutine(currentRoutine);
    }

    static IEnumerator PlayOST (AmbienceSource ambienceSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        ambienceSource.Play();
    }
}
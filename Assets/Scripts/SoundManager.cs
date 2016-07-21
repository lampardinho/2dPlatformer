using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip LaunchMissleSound;
    public AudioClip ExplosionSound;

    private static SoundManager _instance;

    void Start()
    {
        _instance = this;
    }

    public static void MakeLaunchMissleSound()
    {
        AudioSource.PlayClipAtPoint(_instance.LaunchMissleSound, Vector3.zero);
    }

    public static void MakeExplosionSound()
    {
        AudioSource.PlayClipAtPoint(_instance.ExplosionSound, Vector3.zero);
    }

}

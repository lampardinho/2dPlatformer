using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    public GameObject DamageParticles;
    public GameObject DestroyParticles;

    private static ParticleManager _instance;

    void Start()
    {
        _instance = this;
    }

    public static void CreateDamageParticles(Vector2 position)
    {
        PoolManager.Spawn(_instance.DamageParticles, position, Quaternion.identity);
    }

    public static void CreateDestroyParticles(Vector2 position)
    {
        PoolManager.Spawn(_instance.DestroyParticles, position, Quaternion.identity);
    }
}

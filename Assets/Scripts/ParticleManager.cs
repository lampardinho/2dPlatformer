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
        var particle = PoolManager.Spawn(_instance.DamageParticles, position, Quaternion.identity);
        _instance.StartCoroutine(_instance.DespawnParticle(particle,
            particle.GetComponentInChildren<ParticleSystem>().duration + particle.GetComponentInChildren<ParticleSystem>().startLifetime));
    }

    public static void CreateDestroyParticles(Vector2 position)
    {
        var particle = PoolManager.Spawn(_instance.DestroyParticles, position, Quaternion.identity);
        _instance.StartCoroutine(_instance.DespawnParticle(particle,
            particle.GetComponentInChildren<ParticleSystem>().duration + particle.GetComponentInChildren<ParticleSystem>().startLifetime));
    }

    private IEnumerator DespawnParticle(GameObject particle, float duration)
    {
        yield return new WaitForSeconds(duration);
        PoolManager.Despawn(particle);
    }
}

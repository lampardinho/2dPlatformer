using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    public GameObject DestroyParticles;

    private static ParticleManager _instance;

    void Start()
    {
        _instance = this;
    }

    public static void CreateDestroyParticles(Vector2 position)
    {
        var particle = PoolManager.Spawn(_instance.DestroyParticles, position, Quaternion.identity);
        particle.transform.SetParent(_instance.transform);
        var particleSystem = particle.GetComponentInChildren<ParticleSystem>();
        _instance.StartCoroutine(_instance.DespawnParticle(particle, particleSystem.duration + particleSystem.startLifetime));
    }

    private IEnumerator DespawnParticle(GameObject particle, float duration)
    {
        yield return new WaitForSeconds(duration);
        PoolManager.Despawn(particle);
    }
}

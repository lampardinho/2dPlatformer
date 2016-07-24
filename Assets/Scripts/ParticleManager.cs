using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ParticleManager : SingletonBehaviour<ParticleManager>
    {
        [SerializeField] private GameObject _destroyParticles;

        public void CreateDestroyParticles(Vector2 position)
        {
            var particle = PoolManager.Spawn(_destroyParticles, position, Quaternion.identity);
            particle.transform.SetParent(transform);
            var particleSys = particle.GetComponentInChildren<ParticleSystem>();
            StartCoroutine(DespawnParticle(particle, particleSys.duration + particleSys.startLifetime));
        }

        private IEnumerator DespawnParticle(GameObject particle, float duration)
        {
            yield return new WaitForSeconds(duration);
            PoolManager.Despawn(particle);
        }
    }
}
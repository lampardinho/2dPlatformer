using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractAsteroid : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _rotationSpeed;

        protected float Health;
        protected float MoveSpeed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rotationSpeed = Random.Range(-30f, 30f);
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.left*MoveSpeed*Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation + _rotationSpeed*Time.fixedDeltaTime);
        }

        private void OnBecameInvisible()
        {
            PoolManager.Despawn(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            SoundManager.Instance.MakeExplosionSound();

            var missile = coll.gameObject.GetComponent<Missile>();
            if (missile != null)
            {
                Health -= missile.Damage;
            }

            //destroy asteroid
            if (Health <= 0)
            {
                gameObject.SetActive(false);
                ParticleManager.Instance.CreateDestroyParticles(coll.contacts[0].point);
            }
        }
    }
}
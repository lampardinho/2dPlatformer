using UnityEngine;

namespace Assets.Scripts
{
    public class Missile : MonoBehaviour
    {
        [SerializeField] private readonly float _damage = 1;

        [SerializeField] private readonly float _speed = 6;

        private Rigidbody2D _rigidbody;

        public float Damage
        {
            get { return _damage; }
        }

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.right*_speed*Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            gameObject.SetActive(false);
        }

        private void OnBecameInvisible()
        {
            PoolManager.Despawn(gameObject);
        }
    }
}
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private GameObject _missilePrefab;

        [SerializeField] private float _offsetFromBorder = 1f;

        // Cooldown in seconds between two shots
        [SerializeField] private float _shootingRate = 0.25f;

        [SerializeField] private float _speed = 5f;

        private float _x;
        private float _y;
        private Rigidbody2D _rigidbody;
        private float _shootCooldown;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Spawn()
        {
            gameObject.SetActive(true);
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;
        }

        private void Update()
        {
            _x = CrossPlatformInputManager.GetAxis("Horizontal");
            _y = CrossPlatformInputManager.GetAxis("Vertical");

            var isShooting = CrossPlatformInputManager.GetButton("Shoot");
            var canAttack = _shootCooldown <= 0f;
            if (isShooting && canAttack)
            {
                _shootCooldown = _shootingRate;
                var missile = PoolManager.Spawn(_missilePrefab, _rigidbody.position, Quaternion.identity);
                missile.transform.SetParent(transform.parent);
                SoundManager.Instance.MakeLaunchMissleSound();
            }

            if (_shootCooldown > 0)
            {
                _shootCooldown -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            //coordinates of screen borders
            var dist = (transform.position - Camera.main.transform.position).z;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

            var newPosition = transform.position + new Vector3(_x, _y) * _speed * Time.fixedDeltaTime;

            //clamp ship position to prevent going offscreen
            var newX = Mathf.Clamp(newPosition.x, leftBorder + _offsetFromBorder, rightBorder - _offsetFromBorder);
            var newY = Mathf.Clamp(newPosition.y, bottomBorder + _offsetFromBorder, topBorder - _offsetFromBorder);

            _rigidbody.MovePosition(new Vector2(newX, newY));
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            ParticleManager.Instance.CreateDestroyParticles(coll.contacts[0].point);
            Despawn();
        }

        private void Despawn()
        {
            gameObject.SetActive(false);

            Application.Instance.GameOver();
        }
    }
}
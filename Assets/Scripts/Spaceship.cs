using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Spaceship : MonoBehaviour
{
    public GameObject GameWorld;
    public GameObject BulletPrefab;
    private float _x;
    private float _y;
    private float _speedMultiplier = 0.1f;
    private float _offsetFromBorder = 1f;

    // Cooldown in seconds between two shots
    private float _shootingRate = 0.25f;

    // Cooldown
    private float _shootCooldown;

    private Rigidbody2D _rigidbody;

    public bool CanAttack
    {
        get
        {
            return _shootCooldown <= 0f;
        }
    }

    void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	}

    public void Init()
    {
        gameObject.SetActive(true);
    }
	
	
	void Update ()
	{
	    _x = CrossPlatformInputManager.GetAxis("Horizontal");
	    _y = CrossPlatformInputManager.GetAxis("Vertical");

	    var isShooting = CrossPlatformInputManager.GetButton("Shoot");
	    if (isShooting && CanAttack)
	    {
            _shootCooldown = _shootingRate;
            var missle = PoolManager.Spawn(BulletPrefab, _rigidbody.position, Quaternion.identity);
            missle.transform.SetParent(GameWorld.transform);
            SoundManager.MakeLaunchMissleSound();
	    }

        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        var newPosition = transform.position + new Vector3(_x * _speedMultiplier, _y * _speedMultiplier);
        var newX = Mathf.Clamp(newPosition.x, leftBorder + _offsetFromBorder, rightBorder - _offsetFromBorder);
        var newY = Mathf.Clamp(newPosition.y, bottomBorder + _offsetFromBorder, topBorder - _offsetFromBorder);

        _rigidbody.MovePosition(new Vector3(newX, newY));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collided with " + coll.gameObject.name);
        ParticleManager.CreateDestroyParticles(coll.contacts[0].point);
        gameObject.SetActive(false);

        Application.GameOver();
    }
}

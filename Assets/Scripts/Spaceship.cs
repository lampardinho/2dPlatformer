using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Spaceship : MonoBehaviour
{
    public GameObject GameWorld;
    public GameObject BulletPrefab;
    private float _x;
    private float _y;
    private float _speedMultiplier = 0.1f;
    private float _offsetFromBorder = 1f;

    private Rigidbody2D _rigidbody;

	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	
	void Update ()
	{
	    _x = CrossPlatformInputManager.GetAxis("Horizontal");
	    _y = CrossPlatformInputManager.GetAxis("Vertical");

	    var isShooting = CrossPlatformInputManager.GetButtonDown("Shoot");
	    if (isShooting)
	    {
	        var missle = PoolManager.Spawn(BulletPrefab, _rigidbody.position, Quaternion.identity);
            missle.transform.SetParent(GameWorld.transform);
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
        Destroy(gameObject);
    }
}

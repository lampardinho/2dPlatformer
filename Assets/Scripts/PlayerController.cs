using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public GameObject GameWorld;
    public GameObject BulletPrefab;
    private float _x;
    private float _y;
    private float _speedMultiplier = 0.1f;

	void Start ()
    {
	
	}
	
	
	void Update ()
	{
	    _x = CrossPlatformInputManager.GetAxis("Horizontal");
	    _y = CrossPlatformInputManager.GetAxis("Vertical");

        var newPosition = new Vector3(_x * _speedMultiplier, _y * _speedMultiplier);
	    transform.Translate(newPosition);

        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).y;


//        GameWorld.transform.position -= new Vector3(Mathf.Clamp(_x, leftBorder, rightBorder),
//            Mathf.Clamp(_y, bottomBorder, topBorder)) * _speedMultiplier;

	    var isShooting = CrossPlatformInputManager.GetButtonDown("Shoot");
	    if (isShooting)
	    {
	        var bullet = Instantiate(BulletPrefab);
	        bullet.transform.Translate(transform.position + new Vector3(1f, 0));
	    }

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collided with " + coll.gameObject.name);
        Destroy(gameObject);
    }
}

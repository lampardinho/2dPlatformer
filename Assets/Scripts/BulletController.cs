using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.Translate(new Vector3(0.01f, 0));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collided with " + coll.gameObject.name);
        Destroy(gameObject);
    }
}

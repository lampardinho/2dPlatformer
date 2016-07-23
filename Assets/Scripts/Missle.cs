using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private float _speed = 6;

    void OnEnable ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        //_collider.enabled = true;
        Debug.Log("OnEnable " + name);
    }
	
	void FixedUpdate ()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.right * _speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.LogError("collided with " + coll.gameObject.name);
        gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisible " + name);
        PoolManager.Despawn(gameObject);
    }

    void OnDisable()
    {
        Debug.Log("OnDisable " + name);
        //_collider.enabled = false;
    }
}

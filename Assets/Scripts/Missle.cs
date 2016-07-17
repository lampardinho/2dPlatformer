using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private float _speed = 6;

    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
    }
	
	void FixedUpdate ()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.right * _speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.LogError("collided with " + coll.gameObject.name);
        PoolManager.Despawn(gameObject);
    }

    void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisible " + name);
        PoolManager.Despawn(gameObject);
    }

    void OnDisable()
    {
        _collider.enabled = false;
    }
}

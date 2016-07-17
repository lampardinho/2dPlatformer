using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public abstract class AbstractAsteroid : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private float _rotationSpeed;
    protected float _moveSpeed;
    protected float _health;

    void OnEnable ()
    {
        Debug.Log("ENABLE");
        Init();
    }

    protected virtual void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
        _rotationSpeed = Random.Range(-30f, 30f);
    }
	
	void FixedUpdate ()
	{
        _rigidbody.MovePosition(_rigidbody.position + Vector2.left * _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation + _rotationSpeed * Time.fixedDeltaTime);
	}

    void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisible " + name);
        //PoolManager.Despawn(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collided with " + coll.gameObject.name);

        //todo get this value from missle
        _health--;

        if (_health <= 0)
            gameObject.SetActive(false);//PoolManager.Despawn(gameObject);
    }

    void OnDisable()
    {
        Debug.Log("DISABLE");
        _collider.enabled = false;
    }
}

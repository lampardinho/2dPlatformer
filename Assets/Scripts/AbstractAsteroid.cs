using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public abstract class AbstractAsteroid : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _rotationSpeed;

    protected float MoveSpeed;
    protected float Health;

    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotationSpeed = Random.Range(-30f, 30f);
    }

	void FixedUpdate ()
	{
        _rigidbody.MovePosition(_rigidbody.position + Vector2.left * MoveSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation + _rotationSpeed * Time.fixedDeltaTime);
	}

    void OnBecameInvisible()
    {
        //Debug.Log("OnBecameInvisible " + name);
        PoolManager.Despawn(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("collided with " + coll.gameObject.name);

        //todo get this value from missle
        Health--;

        SoundManager.MakeExplosionSound();

        if (Health <= 0)
        {
            gameObject.SetActive(false);
            ParticleManager.CreateDestroyParticles(coll.contacts[0].point);
        }
        else
        {
            //ParticleManager.CreateDamageParticles(coll.contacts[0].point);
        }
    }
}

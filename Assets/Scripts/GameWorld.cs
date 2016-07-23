using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameWorld : MonoBehaviour
{
    public Spaceship Ship;
    public GameObject[] Asteroids;
    public GameObject Explosion;

    private bool _isGameRunning = false;
    private List<GameObject> _spawnedAsteroids = new List<GameObject>();

    public void StartGame ()
    {
        //GameObject spaceship = Instantiate(Spaceship, Vector3.zero, Quaternion.identity) as GameObject;
        //spaceship.GetComponent<Spaceship>().GameWorld = gameObject;
        _isGameRunning = true;
        Ship.Init();
        //StartCoroutine(CreateAsteroids());
	}

    public void StopGame()
    {
        _isGameRunning = false;

        foreach (var asteroid in _spawnedAsteroids)
        {
            PoolManager.Despawn(asteroid);
        }
    }

	void Update()
	{
        //todo delete
        if (Input.GetKeyDown(KeyCode.Z))
		{
			Instantiate(Explosion, Vector3.left, Quaternion.identity);
		}
	}
	
    private IEnumerator CreateAsteroids()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        while (_isGameRunning)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            var type = Random.Range(0, Asteroids.Length - 1);
            var position = new Vector3(rightBorder/* + Random.Range(1f, 5f)*/, Random.Range(bottomBorder + 1, topBorder - 1));
            var asteroid = PoolManager.Spawn(Asteroids[type], position, Quaternion.identity);
            asteroid.transform.SetParent(transform);
            _spawnedAsteroids.Add(asteroid);
        }
    }
}

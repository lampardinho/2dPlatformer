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
        _isGameRunning = true;
        Ship.Init();
        StartCoroutine(CreateAsteroids());
	}

    public void StopGame()
    {
        _isGameRunning = false;

        foreach (var asteroid in _spawnedAsteroids)
        {
            asteroid.SetActive(false);
        }
        _spawnedAsteroids.Clear();
    }
	
    private IEnumerator CreateAsteroids()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));

            if (!_isGameRunning)
                yield break;

            var type = Random.Range(0, Asteroids.Length - 1);
            var position = new Vector3(rightBorder + 1, Random.Range(bottomBorder + 1, topBorder - 1));
            var asteroid = PoolManager.Spawn(Asteroids[type], position, Quaternion.identity);
            asteroid.transform.SetParent(transform);
            _spawnedAsteroids.Add(asteroid);
        }
    }
}

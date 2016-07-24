using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameWorld : MonoBehaviour
    {
        [SerializeField] private Spaceship _ship;

        [SerializeField] private GameObject[] _asteroidPrefabs;

        [SerializeField] private float _minTimeBetweenAsteroids = 0.5f;

        [SerializeField] private float _maxTimeBetweenAsteroids = 1f;

        private readonly List<GameObject> _spawnedAsteroids = new List<GameObject>();
        private bool _isGameRunning;
    
        public void StartGame()
        {
            _isGameRunning = true;
            _ship.Spawn();
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
            //screen borders
            var dist = (transform.position - Camera.main.transform.position).z;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

            while (true)
            {
                //pause between asteroids spawn
                yield return new WaitForSeconds(Random.Range(_minTimeBetweenAsteroids, _maxTimeBetweenAsteroids));

                //stop creating asteroids when game is over
                if (!_isGameRunning)
                    yield break;

                var asteroidType = Random.Range(0, _asteroidPrefabs.Length - 1);
                var spawnPosition = new Vector2(rightBorder + 1, Random.Range(bottomBorder + 1, topBorder - 1));
                var asteroid = PoolManager.Spawn(_asteroidPrefabs[asteroidType], spawnPosition, Quaternion.identity);
                asteroid.transform.SetParent(transform);
                _spawnedAsteroids.Add(asteroid);
            }
        }
    }
}
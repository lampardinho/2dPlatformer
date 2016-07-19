using UnityEngine;
using System.Collections;

public class GameWorld : MonoBehaviour
{
    public GameObject[] Asteroids;
    public GameObject Explosion;

    void Start ()
	{
	    //StartCoroutine(CreateAsteroids());
	}

	void Update()
	{
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

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            var type = Random.Range(0, Asteroids.Length - 1);
            var position = new Vector3(rightBorder/* + Random.Range(1f, 5f)*/, Random.Range(bottomBorder + 1, topBorder - 1));
            var asteroid = PoolManager.Spawn(Asteroids[type], position, Quaternion.identity);
            asteroid.transform.SetParent(transform);
        }
    }
}

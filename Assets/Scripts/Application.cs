using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Application : MonoBehaviour
{
    private static Application _instance;

    void Start ()
    {
	    DontDestroyOnLoad(this);
	}

    public static void GameOver()
    {
        SceneManager.LoadScene("RestartMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Application : MonoBehaviour
{
    public Button RestartButton;
    public GameObject GameControls;
    public GameWorld World;

    private static Application _instance;

    void Start ()
    {
        _instance = this;

        StartGame();

        RestartButton.onClick.AddListener(StartGame);
	}

    public static void GameOver()
    {
        _instance.World.StopGame();
        _instance.RestartButton.gameObject.SetActive(true);
        _instance.GameControls.SetActive(false);
    }

    public static void StartGame()
    {
        _instance.RestartButton.gameObject.SetActive(false);
        _instance.GameControls.SetActive(true);
        _instance.World.StartGame();
    }
}

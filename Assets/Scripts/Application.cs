using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Application : SingletonBehaviour<Application>
    {
        [SerializeField] private Button _restartButton;

        [SerializeField] private GameWorld _world;

        private void Start()
        {
            StartGame();
            _restartButton.onClick.AddListener(StartGame);
        }

        public void GameOver()
        {
            _world.StopGame();
            _restartButton.gameObject.SetActive(true);
        }

        public void StartGame()
        {
            _restartButton.gameObject.SetActive(false);
            _world.StartGame();
        }
    }
}
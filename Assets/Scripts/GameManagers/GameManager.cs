using Tanchiki.Entity;
using UnityEngine;

namespace Tanchiki.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        #region Синглтон
        public static GameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
                //DontDestroyOnLoad(gameObject); // Если нужно сохранять между сценами
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
        #region Инициализация
        private Health playerHealth;
        private void Initialize()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        private void OnEnable()
        {
            playerHealth.onDeath?.AddListener(SetGameOver);
        }
        private void OnDisable()
        {
            playerHealth.onDeath?.RemoveListener(SetGameOver);
        }
        #endregion
        #region Состояния игры
        public enum GameState { Playing, Paused, GameOver, Victory }

        internal GameState currentState;

        public void SetGameState(GameState newState)
        {
            currentState = newState;

            switch (newState)
            {
                case GameState.Playing:
                    Time.timeScale = 1;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0;
                    break;
                case GameState.GameOver:
                    ShowEndScreen();
                    break;
                case GameState.Victory:
                    ShowEndScreen();
                    break;
            }
        }
        public void SetPlaying() => SetGameState(GameState.Playing);
        public void SetPaused() => SetGameState(GameState.Paused);
        public void SetGameOver() => SetGameState(GameState.GameOver);
        public void SetVictory() => SetGameState(GameState.Victory);
        public bool isPaused() { return currentState == GameState.Paused; }
        #endregion

        [SerializeField] private GameObject EndScreen;
        private void ShowEndScreen()
        {
            EndScreen.SetActive(true);
        }
    }
}
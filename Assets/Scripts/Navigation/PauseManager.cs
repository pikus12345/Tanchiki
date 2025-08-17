using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Tanchiki.GameManagers;

namespace Tanchiki.Navigation
{
    public class PauseManager : MonoBehaviour
    {
        
        [Header("UI Settings")]
        [SerializeField] private GameObject pausePanel; // Панель паузы (UI Canvas)
        [SerializeField] private InputActionAsset inputAsset;
        private InputAction pauseAction;

        [Header("Audio Settings")]
        [SerializeField] private bool pauseAudio = true;

        private void Awake()
        {
            pauseAction = inputAsset.FindAction("Pause");
        }
        private void Update()
        {
            if (pauseAction.WasPressedThisFrame())
            {
                TogglePause();
            }
        }
        private void OnEnable()
        {
            inputAsset.FindActionMap("Player").Enable();
        }
        private void OnDisable()
        {
            inputAsset.FindActionMap("Player").Disable();
        }

        // Включить/выключить паузу
        public void TogglePause()
        {
            if(!GameManager.Instance.isPaused())
            {
                GameManager.Instance.SetGameState(GameManager.GameState.Paused);
            }
            else if (GameManager.Instance.isPaused())
            {
                GameManager.Instance.SetGameState(GameManager.GameState.Playing);
            }

            // Остановка/возобновление аудио
            if (pauseAudio)
            {
                AudioListener.pause = (GameManager.Instance.isPaused());
            }

            // Включение/выключение UI панели
            if (pausePanel != null)
            {
                pausePanel.SetActive(GameManager.Instance.isPaused());
            }
        }

        // Принудительно установить паузу (например, из другого скрипта)
        public void SetPause(bool pause)
        {
            if (GameManager.Instance.isPaused() != pause)
            {
                TogglePause();
            }
        }
    }
}
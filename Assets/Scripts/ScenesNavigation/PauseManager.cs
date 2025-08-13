using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

        private bool isPaused = false;

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
            isPaused = !isPaused;

            // Остановка/возобновление времени
            Time.timeScale = isPaused ? 0f : 1f;

            // Остановка/возобновление аудио
            if (pauseAudio)
            {
                AudioListener.pause = isPaused;
            }

            // Включение/выключение UI панели
            if (pausePanel != null)
            {
                pausePanel.SetActive(isPaused);
            }
        }

        // Принудительно установить паузу (например, из другого скрипта)
        public void SetPause(bool pause)
        {
            if (isPaused != pause)
            {
                TogglePause();
            }
        }
    }
}
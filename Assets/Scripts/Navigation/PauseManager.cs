using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Tanchiki.GameManagers;
using System.Linq.Expressions;

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
        #region Синглтон
        public static PauseManager Instance;
        private void Awake()
        {
            if (Instance == null) 
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
        private void Start()
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
            GameManager.Instance.TogglePause();

            /*if(!GameManager.Instance.isPaused())
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
            */
        }
        public void DoPaused()
        {
            pausePanel?.SetActive(true);
            if (pauseAudio)
            {
                AudioListener.pause = true;
            }
        }
        public void DoPlaying()
        {
            pausePanel?.SetActive(false);
            if (pauseAudio)
            {
                AudioListener.pause = false;
            }
        }

        // Принудительно установить паузу (например, из другого скрипта) LEGACY
        //public void SetPause(bool pause)
        //{
        //    if (GameManager.Instance.isPaused() != pause)
        //    {
        //        TogglePause();
        //    }
        //}
    }
}
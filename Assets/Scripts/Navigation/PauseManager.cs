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
        [SerializeField] private GameObject pausePanel; // ������ ����� (UI Canvas)
        [SerializeField] private InputActionAsset inputAsset;
        private InputAction pauseAction;

        [Header("Audio Settings")]
        [SerializeField] private bool pauseAudio = true;
        #region ��������
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

        // ��������/��������� �����
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

            // ���������/������������� �����
            if (pauseAudio)
            {
                AudioListener.pause = (GameManager.Instance.isPaused());
            }

            // ���������/���������� UI ������
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

        // ������������� ���������� ����� (��������, �� ������� �������) LEGACY
        //public void SetPause(bool pause)
        //{
        //    if (GameManager.Instance.isPaused() != pause)
        //    {
        //        TogglePause();
        //    }
        //}
    }
}
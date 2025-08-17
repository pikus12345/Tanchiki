using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Tanchiki.GameManagers;

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

        // ��������/��������� �����
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
        }

        // ������������� ���������� ����� (��������, �� ������� �������)
        public void SetPause(bool pause)
        {
            if (GameManager.Instance.isPaused() != pause)
            {
                TogglePause();
            }
        }
    }
}
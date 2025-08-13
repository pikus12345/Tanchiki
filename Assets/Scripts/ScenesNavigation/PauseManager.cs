using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

        // ��������/��������� �����
        public void TogglePause()
        {
            isPaused = !isPaused;

            // ���������/������������� �������
            Time.timeScale = isPaused ? 0f : 1f;

            // ���������/������������� �����
            if (pauseAudio)
            {
                AudioListener.pause = isPaused;
            }

            // ���������/���������� UI ������
            if (pausePanel != null)
            {
                pausePanel.SetActive(isPaused);
            }
        }

        // ������������� ���������� ����� (��������, �� ������� �������)
        public void SetPause(bool pause)
        {
            if (isPaused != pause)
            {
                TogglePause();
            }
        }
    }
}
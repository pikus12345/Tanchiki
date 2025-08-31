using UnityEngine;
using UnityEngine.InputSystem;
using Tanchiki.Entity;

namespace Tanchiki.PlayerControl
{
    public class PlayerTurretRotation : TurretRotation
    {
        [Header("Input")]
        [SerializeField] private InputActionAsset inputActions;
        private InputAction m_aimAction;

        private Camera m_mainCamera;
        

        internal override void Awake()
        {
            base.Awake();
            m_mainCamera = Camera.main;

            if (inputActions != null)
            {
                m_aimAction = inputActions.FindAction("AimByMousePosition");
            }
            else
            {
                Debug.LogError("InputActions Asset is not assigned!", this);
            }
        }

        private void Update()
        {
            RotateTowerTowardsMouse();
        }
        
        private void RotateTowerTowardsMouse()
        {
            if (m_aimAction == null) return;

            Vector2 mousePosition = m_aimAction.ReadValue<Vector2>();
            Vector2 worldMousePosition = m_mainCamera.ScreenToWorldPoint(mousePosition);

            RotateToPoint(worldMousePosition);
        }

        private void OnEnable()
        {
            if (m_aimAction != null)
                m_aimAction.Enable();
        }

        private void OnDisable()
        {
            if (m_aimAction != null)
                m_aimAction.Disable();
        }
    }
}
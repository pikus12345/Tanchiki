using UnityEngine;
using UnityEngine.InputSystem;
using Tanchiki.Entity;
using YG;

namespace Tanchiki.PlayerControl
{
    public class PlayerTurretRotation : TurretRotation
    {
        [Header("Input")]
        [SerializeField] private InputActionAsset inputActions;
        private InputAction m_aimAction;
        private InputAction m_aimActionByStick;

        private Camera m_mainCamera;
        

        internal override void Awake()
        {
            base.Awake();
            m_mainCamera = Camera.main;

            if (inputActions != null)
            {
                m_aimAction = inputActions.FindAction("AimByMousePosition");
                m_aimActionByStick = inputActions.FindAction("AimByStick");
            }
            else
            {
                Debug.LogError("InputActions Asset is not assigned!", this);
            }
        }

        private void Update()
        {
            if (YG2.envir.isDesktop)
            {
                RotateTowerTowardsMouse();
            }
            else
            {
                RotateTowerByStick();
            }
            
        }
        
        private void RotateTowerTowardsMouse()
        {
            if (m_aimAction == null) return;

            Vector2 mousePosition = m_aimAction.ReadValue<Vector2>();
            Vector2 worldMousePosition = m_mainCamera.ScreenToWorldPoint(mousePosition);

            RotateToPoint(worldMousePosition);
        }
        private void RotateTowerByStick()
        {
            if (m_aimActionByStick == null) return;
            Vector2 value = m_aimActionByStick.ReadValue<Vector2>();
            if (value.magnitude != 0)
            {
                RotateToAngle(Vector2.SignedAngle(Vector2.right, value));
                GetComponent<Shooting>().Shoot();
            }
            
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
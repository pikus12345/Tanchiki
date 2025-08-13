using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanchiki.PlayerControl
{
    public class TowerRotation : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionAsset inputActions;
        private InputAction m_aimAction;

        [Header("Settings")]
        [SerializeField] private float m_rotationSpeed = 10f;
        [SerializeField] private bool m_smoothRotation = true;

        private Camera m_mainCamera;
        private Transform m_turretTransform;

        private void Awake()
        {
            m_turretTransform = transform;
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

            Vector2 direction = worldMousePosition - (Vector2)m_turretTransform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (m_smoothRotation)
            {
                // ѕлавный поворот
                float currentAngle = m_turretTransform.eulerAngles.z;
                float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, m_rotationSpeed * Time.deltaTime);
                m_turretTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            else
            {
                // ћгновенный поворот
                m_turretTransform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
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
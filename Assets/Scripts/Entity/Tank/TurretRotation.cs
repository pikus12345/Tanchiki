using UnityEngine;

namespace Tanchiki.Entity
{
    public class TurretRotation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected float m_rotationSpeed = 10f;
        [SerializeField] protected bool m_smoothRotation = true;
        protected Transform m_turretTransform;
        internal void Awake()
        {
            m_turretTransform = transform;
        }
        internal void RotateToPoint(Vector2 worldPosition)
        {
            Vector2 direction = worldPosition - (Vector2)m_turretTransform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            RotateToAngle(targetAngle);
        }
        internal void RotateToAngle(float targetAngle)
        {
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
    }
}

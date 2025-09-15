using UnityEngine;

namespace Tanchiki.Entity
{
    public class TankControl : MonoBehaviour
    {
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_rotationSpeed;
        [SerializeField] protected float m_maxSpeed;
        [SerializeField] protected float m_speedDrag;

        protected bool isFreezed;

        internal Rigidbody2D m_Rigidbody;

        protected float m_currentSpeed;
        internal virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }
        internal void Moving(float moveVectorY)
        {
            if (isFreezed) return;
            Vector2 movement = transform.right * Time.fixedDeltaTime * m_moveSpeed;
            m_currentSpeed += moveVectorY;
            m_currentSpeed = Mathf.MoveTowards(m_currentSpeed, 0, Time.fixedDeltaTime * m_speedDrag);
            m_currentSpeed = Mathf.Clamp(m_currentSpeed, -m_maxSpeed, m_maxSpeed);
            
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement * m_currentSpeed);
        }
        internal void Rotating(float moveVectorX)
        {
            if (isFreezed) return;
            // Âû÷èñëÿåì óãîë ïîâîðîòà
            float rotation = -moveVectorX * m_rotationSpeed * Time.fixedDeltaTime;

            // Ïðèìåíÿåì ïîâîðîò ê Rigidbody
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation + rotation);
        }
        internal void RotateToAngle(float targetAngle)
        {
            float maxRotation = m_rotationSpeed * Time.fixedDeltaTime;
            float currentAngle = m_Rigidbody.rotation;
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, maxRotation);
            float diff = Mathf.DeltaAngle(currentAngle, angle);
            m_Rigidbody.MoveRotation(angle);
        }
        internal float DifferenceToTargetAngle(float targetAngle)
        {
            float maxRotation = m_rotationSpeed;
            float currentAngle = m_Rigidbody.rotation;
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, maxRotation);
            float diff = Mathf.DeltaAngle(currentAngle, angle);
            return diff;
        }
        internal float DifferenceToTargetAngleByPosition(Vector2 worldPosition)
        {
            Vector2 direction = worldPosition - (Vector2)m_Rigidbody.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return DifferenceToTargetAngle(targetAngle);
        }
        internal void RotateToPoint(Vector2 worldPosition)
        {
            Vector2 direction = worldPosition - (Vector2)m_Rigidbody.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            RotateToAngle(targetAngle);
        }
        public float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            // Считаем, где находится value относительно исходного диапазона (это и есть нормализация)
            float t = (value - fromMin) / (fromMax - fromMin);
            // Переносим это относительное положение в новый диапазон
            return Mathf.Lerp(toMin, toMax, t);

            // Или одной строкой:
            // return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }
        internal void Freeze()
        {
            isFreezed = true;
            transform.GetComponentInChildren<TurretRotation>().enabled = false;
            Shooting shooting = transform.GetComponentInChildren<Shooting>();
            if (shooting != null)
            {
                shooting.enabled = false;
            }
        }
        internal void Unfreeze()
        {
            isFreezed = false;
            transform.GetComponentInChildren<TurretRotation>().enabled = true;
            Shooting shooting = transform.GetComponentInChildren<Shooting>();
            if (shooting != null)
            {
                shooting.enabled = true;
            }
        }
    }
}

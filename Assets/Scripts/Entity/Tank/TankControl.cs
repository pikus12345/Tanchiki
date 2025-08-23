using UnityEngine;

namespace Tanchiki.Entity
{
    public class TankControl : MonoBehaviour
    {
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_rotationSpeed;
        [SerializeField] protected float m_maxSpeed;
        [SerializeField] protected float m_speedDrag;

        internal Rigidbody2D m_Rigidbody;

        protected float m_currentSpeed;
        internal virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }
        internal void Moving(float moveVectorY)
        {
            Vector2 movement = transform.right * Time.fixedDeltaTime * m_moveSpeed;
            m_currentSpeed += moveVectorY;
            m_currentSpeed = Mathf.MoveTowards(m_currentSpeed, 0, Time.fixedDeltaTime * m_speedDrag);
            m_currentSpeed = Mathf.Clamp(m_currentSpeed, -m_maxSpeed, m_maxSpeed);
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement * m_currentSpeed);
        }
        internal void Rotating(float moveVectorX)
        {
            // Вычисляем угол поворота
            float rotation = -moveVectorX * m_rotationSpeed * Time.fixedDeltaTime;

            // Применяем поворот к Rigidbody
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation + rotation);
        }
    }
}

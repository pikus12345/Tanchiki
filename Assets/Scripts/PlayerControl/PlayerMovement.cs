using Tanchiki.Entity;
using Tanchiki.GameManagers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanchiki.PlayerControl
{
    public class PlayerMovement : TankControl
    {
        [SerializeField] InputActionAsset inputActions;

        private InputAction m_moveAction;
        private Vector2 m_moveVector;
        private Rigidbody2D m_Rigidbody;

        [SerializeField] float m_moveSpeed;
        [SerializeField] float m_rotationSpeed;
        [SerializeField] float m_maxSpeed;
        [SerializeField] float m_speedDrag;

        float m_currentSpeed;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            if (inputActions == null)
            {
                Debug.LogError("InputActions Asset is not assigned!", this);
                return;
            }
            m_moveAction = inputActions.FindAction("Move");
        }
        private void Update()
        {
            m_moveVector = m_moveAction.ReadValue<Vector2>();
        }
        private void FixedUpdate()
        {
            Moving();
            Rotating();
        }
        private void Moving()
        {
            Vector2 movement = transform.right * Time.fixedDeltaTime * m_moveSpeed;
            m_currentSpeed += m_moveVector.y;
            m_currentSpeed = Mathf.MoveTowards(m_currentSpeed, 0, Time.fixedDeltaTime * m_speedDrag);
            m_currentSpeed = Mathf.Clamp(m_currentSpeed, -m_maxSpeed, m_maxSpeed);
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement * m_currentSpeed);

            // Ограничение скорости
            




            //if (m_Rigidbody.linearVelocity.magnitude > m_maxSpeed)
            //{
            //    m_Rigidbody.linearVelocity = m_Rigidbody.linearVelocity.normalized * m_maxSpeed;
            //}
        }
        private void Rotating()
        {
            // Вычисляем угол поворота
            float rotation = -m_moveVector.x * m_rotationSpeed * Time.fixedDeltaTime;

            // Применяем поворот к Rigidbody
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation + rotation);
        }
        private void OnEnable()
        {
            inputActions.FindActionMap("Player").Enable();
        }
        private void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Finish")) 
            {
                GameManager.Instance.SetVictory();
            }
        }
    }
}
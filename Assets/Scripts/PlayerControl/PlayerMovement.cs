using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanchiki.PlayerMovement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] InputActionAsset inputActions;

        private InputAction m_moveAction;
        private Vector2 m_moveVector;
        private Rigidbody2D m_Rigidbody;

        [SerializeField] float m_moveSpeed;
        [SerializeField] float m_rotationSpeed;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();

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
            
            // Вычисляем вектор движения (вперед/назад относительно текущего направления)
            Vector2 movement = transform.right * m_moveVector.y * Time.fixedDeltaTime * m_moveSpeed;

            // Применяем движение к Rigidbody
            m_Rigidbody.AddForce(movement);
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
    }
}
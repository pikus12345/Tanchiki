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
        protected void Moving()
        {
            Moving(m_moveVector.y);
        }
        protected void Rotating()
        {
            Rotating(m_moveVector.x);
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
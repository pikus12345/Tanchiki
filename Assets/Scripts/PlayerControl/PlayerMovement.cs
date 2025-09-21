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

        [SerializeField] private float rotateCorrection;
        

        internal override void Awake()
        {
            base.Awake();
            if (inputActions == null)
            {
                Debug.LogError("InputActions Asset is not assigned!", this);
                return;
            }
            m_moveAction = inputActions.FindAction("Move");
        }
        internal void Update()
        {
            m_moveVector = m_moveAction.ReadValue<Vector2>();
        }
        private void FixedUpdate()
        {
            //Moving();
            //Rotating();
            MovingUntilRotate(m_moveVector);
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
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Finish")) 
        //    {
        //        GameManager.Instance.SetVictory();
        //    }
        //}
        private void MovingUntilRotate(Vector2 moveVector)
        {
            if (moveVector.magnitude == 0) return;
            float targetAngle = Vector2.SignedAngle(Vector2.right, moveVector);

            if (Mathf.Abs(Mathf.DeltaAngle(m_Rigidbody.rotation, targetAngle)) > rotateCorrection)
            {
                RotateToAngle(targetAngle);
            }
            else
            {
                RotateToAngle(targetAngle);
                Moving(moveVector.magnitude);
            }
        }
    }
}
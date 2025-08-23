using UnityEngine;

namespace Tanchiki.Entity
{
    #region Класс эдитора
    public class TankAIControl : TankControl
    {
        [Header("AI Settings")]
        [SerializeField] private PatrolRoute patrolRoute;
        private int currentPatrolPoint;
        private bool doPatrol;

        [SerializeField] private float viewDistance;
        [SerializeField] internal float stopDistance;
        [SerializeField] internal float rotateCorrection;
        [SerializeField] private LayerMask targetViewLayers;
        internal TurretRotation turretRotation;
        internal Transform target;

        private TankAIFSM FSM;
        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            doPatrol = patrolRoute;

            target = GameObject.FindGameObjectWithTag("Player").transform;

            turretRotation = transform.GetComponentInChildren<TurretRotation>();

            FSM = new TankAIFSM(this);
            FSM.ChangeState(new Idle());
        }
        private void Update()
        {
            FSM.UpdateState();
            CheckState();
        }
        private void CheckState()
        {
            TankAIBaseState state = FSM.currentState;
            if(isPlayerVisible())
            {
                FSM.ChangeState(new Attack());
            }
            else
            {
                if (doPatrol)
                {
                    FSM.ChangeState(new Patrol());
                }
                else
                {
                    FSM.ChangeState(new Idle());
                }
            }
        }
        private bool isPlayerVisible()
        {
            Vector2 direction = (target.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance, targetViewLayers);
            if (hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stopDistance);
        }
    }
    #endregion
    #region FSM
    #region Реализация класса FSM
    // Интерфейс для состояний (для удобства работы)
    public interface ITankAIState
    {
        void Enter(TankAIControl tank);
        void Update(TankAIControl tank);
        void Exit(TankAIControl tank);
    }

    // Абстрактный класс состояния
    public abstract class TankAIBaseState : ITankAIState
    {
        public abstract void Enter(TankAIControl tank);
        public abstract void Update(TankAIControl tank);
        public abstract void Exit(TankAIControl tank);
    }

    // Класс FSM для управления состояниями
    public class TankAIFSM
    {
        internal TankAIBaseState currentState;
        TankAIControl tank;
        public TankAIFSM(TankAIControl tank)
        {
            this.tank = tank;
        }

        public void ChangeState(TankAIBaseState newState)
        {
            if (currentState?.GetType() == newState?.GetType()) { return; }
            currentState?.Exit(tank);
            currentState = newState;
            currentState?.Enter(tank);
        }

        public void UpdateState()
        {
            currentState?.Update(tank);
        }
    }
    #endregion
    #region Классы состояний
    public class Idle : TankAIBaseState
    {
        public override void Enter(TankAIControl tank) 
        {
            tank.Moving(0f);
            tank.Rotating(0f);
        }
        public override void Update(TankAIControl tank) 
        {
            tank.turretRotation.RotateToAngle(0f);
        }
        public override void Exit(TankAIControl tank) { }
    }
    public class Patrol : TankAIBaseState
    {
        public override void Enter(TankAIControl tank) { }
        public override void Update(TankAIControl tank) 
        {
            tank.turretRotation.RotateToAngle(0f);
        }
        public override void Exit(TankAIControl tank) { }
    }
    public class Attack : TankAIBaseState
    {
        public override void Enter(TankAIControl tank) 
        { 
            Debug.Log("Attack State Entered"); 
        }
        public override void Update(TankAIControl tank) 
        {
            Debug.Log("UPDATE ATTACK");
            tank.turretRotation.RotateToPoint(tank.target.position);
        }
        public override void Exit(TankAIControl tank) { }
    }
    #endregion
    #endregion

}
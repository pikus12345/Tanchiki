using UnityEngine;

namespace Tanchiki.Entity
{
    #region Класс эдитора
    public class TankAIControl : TankControl
    {
        [Header("AI Settings")]
        [SerializeField] internal PatrolRoute patrolRoute;
        internal int currentPatrolPoint;
        private bool doPatrol;

        [SerializeField] private float viewDistance;
        [SerializeField] internal float shootDistance;
        [SerializeField] internal float stopDistance;
        [SerializeField] internal float rotateCorrection;
        [SerializeField] private LayerMask targetViewLayers;
        [SerializeField] private float seenMemoryTime;

        internal float timeBeforeLastSeenPlayer = Mathf.Infinity;
        internal TurretRotation turretRotation;
        internal Shooting shooting;
        internal Transform target;

        private TankAIFSM FSM;
        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            doPatrol = patrolRoute;

            GetComponent<Health>().onTakeDamage.AddListener(() => PlayerSeen());

            target = GameObject.FindGameObjectWithTag("Player").transform;

            turretRotation = transform.GetComponentInChildren<TurretRotation>();
            shooting = transform.GetComponentInChildren<Shooting>();

            FSM = new TankAIFSM(this);
            FSM.ChangeState(new Idle());
        }
        private void Update()
        {
            FSM.UpdateState();
            CheckState();
        }
        private void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }
        private void CheckState()
        {
            TankAIBaseState state = FSM.currentState;
            timeBeforeLastSeenPlayer += Time.deltaTime;
            if (isPlayerVisible())
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
                    PlayerSeen();
                    return true;
                }
            }
            if(timeBeforeLastSeenPlayer <= seenMemoryTime)
            {
                return true;
            }
            return false;
        }
        private void PlayerSeen()
        {
            timeBeforeLastSeenPlayer = 0;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
            Gizmos.color = Color.indianRed;
            Gizmos.DrawWireSphere(transform.position, shootDistance);
            Gizmos.color = Color.yellowGreen;
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
        void FixedUpdate(TankAIControl tank);
        void Exit(TankAIControl tank);
    }

    // Абстрактный класс состояния
    public abstract class TankAIBaseState : ITankAIState
    {
        public abstract void Enter(TankAIControl tank);
        public abstract void Update(TankAIControl tank);
        public abstract void FixedUpdate(TankAIControl tank);
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
        public void FixedUpdateState()
        {
            currentState?.FixedUpdate(tank);
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
            tank.turretRotation.RotateToAngle(tank.m_Rigidbody.rotation);
        }
        public override void FixedUpdate(TankAIControl tank) { }
        public override void Exit(TankAIControl tank) { }
    }
    public class Patrol : TankAIBaseState
    {
        Vector2 target = Vector2.zero;
        public override void Enter(TankAIControl tank) { }
        public override void Update(TankAIControl tank) 
        {
            target = tank.patrolRoute.GetWaypoints()[tank.currentPatrolPoint];
            tank.turretRotation.RotateToAngle(tank.m_Rigidbody.rotation);
            if(Vector2.Distance(tank.transform.position, target) < 0.2f)
            {
                if (tank.currentPatrolPoint != tank.patrolRoute.GetWaypoints().Count-1)
                {
                    tank.currentPatrolPoint++;
                }
                else
                {
                    tank.currentPatrolPoint = 0;
                }
                
            }
        }
        public override void FixedUpdate(TankAIControl tank) 
        {
            if (Mathf.Abs(tank.DifferenceToTargetAngleByPosition(target)) > tank.rotateCorrection)
            {
                tank.RotateToPoint(target);
            }
            else
            {
                tank.Moving(1f);
            }
        }
        public override void Exit(TankAIControl tank) { }
    }
    public class Attack : TankAIBaseState
    {
        public override void Enter(TankAIControl tank) { }
        public override void Update(TankAIControl tank) 
        {
            tank.turretRotation.RotateToPoint(tank.target.position);
            if (Vector2.Distance(tank.transform.position, tank.target.position) < tank.shootDistance)
            {
                tank.shooting.Shoot();
            }
        }
        public override void FixedUpdate(TankAIControl tank) 
        {
            if (Mathf.Abs(tank.DifferenceToTargetAngleByPosition(tank.target.position)) > tank.rotateCorrection)
            {
                tank.RotateToPoint(tank.target.position);
            }
            else if(Vector2.Distance(tank.transform.position, tank.target.position) > tank.stopDistance)
            {
                tank.Moving(1f);
            }
            
        }
        public override void Exit(TankAIControl tank) {}
    }
    #endregion
    #endregion

}
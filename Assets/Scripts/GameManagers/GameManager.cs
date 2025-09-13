using Tanchiki.Entity;
using Tanchiki.Navigation;
using Tanchiki.PlayerControl;
using UnityEngine;

namespace Tanchiki.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
                //DontDestroyOnLoad(gameObject); // Если нужно сохранять между сценами
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
        #region Realization
        private Health playerHealth;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private EndScreenShower endScreen;
        private void Initialize()
        {
            InitializeGameStateMachine();
        }
        private void OnEnable()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            playerHealth.onDeath?.AddListener(SetGameOver);
        }
        private void OnDisable()
        {
            playerHealth.onDeath?.RemoveListener(SetGameOver);
        }
        #endregion
        #region FSM
        #region FSM Realization
        public abstract class GameState
        {
            public abstract void Enter();
            public abstract void Update();
            public abstract void Exit();
        }
        public interface IFinalGameState { }
        public class GameStateMachine
        {
            internal GameState currentState;
            public void ChangeState(GameState newState)
            {
                if (currentState is IFinalGameState) return;
                currentState?.Exit();
                currentState = newState;
                currentState?.Enter();
            }
            public void Update() => currentState?.Update();
        }
        #endregion
        #region States
        public class Playing : GameState
        {
            public override void Enter() {
                PauseManager.Instance.DoPlaying();
                Debug.Log("Playing State Entered"); 
            }
            public override void Update() => Debug.Log("Playing State Update");
            public override void Exit() => Debug.Log("Playing State Exited");
        }
        public class Paused : GameState
        {
            public override void Enter() 
            { 
                PauseManager.Instance.DoPaused();
                Debug.Log("Paused State Entered"); 
            }

            public override void Update() => Debug.Log("Paused State Update");
            public override void Exit() => Debug.Log("Paused State Exited");
        }
        public class GameOver : GameState, IFinalGameState
        {
            public override void Enter() 
            {
                Instance.backgroundMusic.Stop();
                Instance.endScreen.ShowEndScreen(false);
                Debug.Log("GameOver State Entered");
            }
            public override void Update() => Debug.Log("GameOver State Update");
            public override void Exit() => Debug.Log("GameOver State Exited");
        }
        public class Victory : GameState, IFinalGameState
        {
            public override void Enter() 
            {
                Instance.endScreen.ShowEndScreen(true);
                Instance.playerHealth.GetComponent<PlayerMovement>().enabled = false;
                LevelManager.CompleteLevel();
                Debug.Log("Victory State Entered"); 
            }
            public override void Update() => Debug.Log("Victory State Update");
            public override void Exit() => Debug.Log("Victory State Exited");
        }
        #endregion
        #region FSM Control
        internal GameStateMachine stateMachine;
        private void InitializeGameStateMachine()
        {
            stateMachine = new GameStateMachine();
            SetGameState(new Playing());
        }
        public void SetGameState(GameState newState)
        {
            stateMachine.ChangeState(newState);
        }
        public void TogglePause()
        {
            if (stateMachine?.currentState is Playing)
            {
                stateMachine.ChangeState(new Paused());
            }
            else if(stateMachine?.currentState is Paused)
            {
                stateMachine.ChangeState(new Playing());
            }
        }
        public void SetPlaying() => SetGameState(new Playing());
        public void SetPaused() => SetGameState(new Paused());
        public void SetGameOver() => SetGameState(new GameOver());
        public void SetVictory() => SetGameState(new Victory());
        #endregion
        #endregion
    }
}
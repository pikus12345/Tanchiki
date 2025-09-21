using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tanchiki.GameManagers.Objectives
{
    public abstract class Objective : MonoBehaviour
    {
        [SerializeField] internal string objectiveDescription;
        public event Action<Objective> OnObjectiveCompleted;
        public bool IsCompleted;

        private void Start()
        {
            Initialize();
        }
        protected abstract void Initialize();
        protected abstract void Cleanup();
        public virtual string Description() => objectiveDescription;
        protected void CompleteTask()
        {
            if (IsCompleted) { return; }

            IsCompleted = true;
            OnObjectiveCompleted?.Invoke(this);
            Cleanup();
        }
        protected virtual void OnDestroy()
        {
            Cleanup();
        }
    }

}

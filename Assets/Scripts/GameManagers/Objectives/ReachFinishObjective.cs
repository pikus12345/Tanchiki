
using System;
using UnityEngine;

namespace Tanchiki.GameManagers.Objectives
{
    public class ReachFinishObjective : Objective
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private string finishTag = "Finish";
        private Collider2D finishCollider;

        protected override void Initialize()
        {
            
            //найти коллайдер финиша
            finishCollider = GameObject.FindGameObjectWithTag(finishTag).GetComponent<Collider2D>();
            if (finishCollider == null)
            {
                Debug.LogError("Finish collider not assigned!");
                return;
            }
            //создать компонент на финише и подписаться на событие
            finishCollider.gameObject.AddComponent<FinishTrigger>().OnPlayerEnter += HandlePlayerReachFinish;
        }
        protected override void Cleanup()
        {
            FinishTrigger trigger = null;
            try
            {
                finishCollider.TryGetComponent<FinishTrigger>(out trigger);
            }
            catch { }
            

            if (trigger != null)
            {
                trigger.OnPlayerEnter -= HandlePlayerReachFinish;
            }
        }
        private void HandlePlayerReachFinish(GameObject player)
        {
            if (player.CompareTag(playerTag))
            {
                CompleteTask();
            }
        }


    }
    public class FinishTrigger : MonoBehaviour
    {
        public event Action<GameObject> OnPlayerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnter?.Invoke(other.gameObject);
            }
        }
    }
}
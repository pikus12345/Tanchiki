
using System;
using UnityEngine;

namespace Tanchiki.GameManagers.Objectives
{
    public class ReachFinishObjective : Objective
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private Collider2D finishCollider;

        protected override void Initialize()
        {
            if (finishCollider == null)
            {
                Debug.LogError("Finish collider not assigned!");
                CompleteTask();
                return;
            }
            //создать компонент на финише и подписаться на событие
            finishCollider.gameObject.AddComponent<FinishTrigger>().OnPlayerEnter += HandlePlayerReachFinish;
        }
        protected override void Cleanup()
        {
            var trigger = finishCollider.GetComponent<FinishTrigger>();
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
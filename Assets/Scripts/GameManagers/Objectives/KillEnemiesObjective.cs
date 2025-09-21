using Tanchiki.Entity;
using UnityEngine;

namespace Tanchiki.GameManagers.Objectives
{
    public class KillEnemiesObjective : Objective
    {
        [SerializeField] private int requiredKills = 10;
        [SerializeField] private string enemyTag = "Enemy";

        private int currentKills = 0;

        protected override void Initialize()
        {
            Health.OnAnyDeath += HandleEnemyDeath;
        }
        protected override void Cleanup()
        {
            Health.OnAnyDeath -= HandleEnemyDeath;
        }

        private void HandleEnemyDeath(GameObject enemy, Health healthComponent)
        {
            if (enemy.CompareTag(enemyTag))
            {
                currentKills++;
                if (currentKills >= requiredKills)
                {
                    CompleteTask();
                }
            }
        }
    }
}
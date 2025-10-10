using Tanchiki.Entity;
using UnityEngine;

namespace Tanchiki.GameManagers.Objectives
{
    public class KillBossObjective : Objective
    {
        [SerializeField] private int requiredKills = 1;
        [SerializeField] private string enemyTag = "Enemy";

        private int currentKills = 0;

        protected override void Initialize()
        {
            Health.OnBossDeath += HandleEnemyDeath;
        }
        protected override void Cleanup()
        {
            Health.OnBossDeath -= HandleEnemyDeath;
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
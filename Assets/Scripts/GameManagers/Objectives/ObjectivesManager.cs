using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tanchiki.GameManagers.Objectives
{
    public class ObjectivesManager : MonoBehaviour
    {
        private List<Objective> objectives = new List<Objective>();
        private int requiredObjectives;
        private int completedObjectives;

        public event Action OnObjectivesCompleted;

        private void Start()
        {
            InitializeObjectives();
        }
        private void InitializeObjectives()
        {
            //собираем задачи на уровне
            objectives.AddRange(FindObjectsByType<Objective>(FindObjectsSortMode.None));
            //считаем сколько необходимо выполненных задач для прохождения уровня
            requiredObjectives = objectives.Count;
            //подписываемся на завершение задач
            foreach (Objective objective in objectives) 
            {
                objective.OnObjectiveCompleted += HandleObjectiveCompleted;
            }

            Debug.Log($"Level initialized with {requiredObjectives} objectives");
        }
        private void HandleObjectiveCompleted(Objective objective)
        {
            completedObjectives++;
            Debug.Log($"Objective {objective.objectiveDescription} is completed");
            if (completedObjectives >= requiredObjectives)
            {
                CompleteObjective();
            }
        }
        private void CompleteObjective() 
        {
            OnObjectivesCompleted?.Invoke();
            foreach (Objective objective in objectives)
            {
                objective.OnObjectiveCompleted -= HandleObjectiveCompleted;
            }
            Debug.Log("Objectives completed!");
        }

    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tanchiki.GameManagers.Objectives
{
    public class ObjectivesManager : MonoBehaviour
    {
        public List<Objective> objectives = new List<Objective>();
        private int requiredObjectives;
        private int completedObjectives;

        public event Action OnObjectivesCompleted;
        public event Action OnAnyObjectiveCompleted;

        private void Awake()
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
            OnAnyObjectiveCompleted.Invoke();
            Debug.Log($"Objective {objective.objectiveDescription} is completed");
            if (completedObjectives >= requiredObjectives)
            {
                CompleteObjectives();
            }
        }
        private void CompleteObjectives() 
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
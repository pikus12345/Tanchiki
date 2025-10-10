using System.Collections.Generic;
using Tanchiki.GameManagers.Objectives;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Tanchiki.Navigation.Objectives
{
    public class ObjDisplayFactory : MonoBehaviour
    {
        [SerializeField] private Transform objectivesList;
        [SerializeField] private GameObject objectivePrefab;
        [SerializeField] private Sprite completedSprite;
        [SerializeField] private Sprite notCompletedSprite;

        private List<GameObject> createdObjs = new List<GameObject>();
        internal void CreateObjective(string text, bool isCompleted)
        {
            GameObject objective = Instantiate(objectivePrefab, objectivesList);
            objective.GetComponentInChildren<TMP_Text>().text = text;
            Sprite sprite = notCompletedSprite;
            if (isCompleted)
            {
                sprite = completedSprite;
            }
            //Debug.Log(objective.transform.Find("Mark"));
            objective.transform.Find("Image").Find("Mark").GetComponent<Image>().sprite = sprite;

            createdObjs.Add(objective);
        }
        internal void SetupObjs(List<Objective> objs)
        {
            foreach (Objective obj in objs) 
            {
                CreateObjective(obj.Description(), obj.IsCompleted);
            }
        }
        /*internal void DestroyObj()
        {

        }*/
        internal void DestroyAllObjs()
        {
            foreach (var obj in createdObjs)
            {
                Destroy(obj);
            }
            createdObjs.Clear();
        }
    }
}

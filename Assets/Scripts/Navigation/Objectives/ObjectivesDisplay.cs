using UnityEngine;
using Tanchiki.GameManagers.Objectives;
using TMPro;
namespace Tanchiki.Navigation.Objectives
{
    public class ObjectivesDisplay : MonoBehaviour
    {
        private ObjectivesManager objectivesManager;
        private ObjDisplayFactory objDisplayFactory;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            objectivesManager = FindAnyObjectByType<ObjectivesManager>();
            objDisplayFactory = FindAnyObjectByType<ObjDisplayFactory>();
            if(objDisplayFactory == null | objectivesManager == null)
            {
                Debug.Log("OM or ODF not assigned!");
                return;
            }
            objectivesManager.OnAnyObjectiveCompleted += Refresh;
            Refresh();
        }
        private void OnDestroy()
        {
            objectivesManager.OnAnyObjectiveCompleted -= Refresh;
        }
        public void Refresh()
        {
            //Debug.Log("Refresh "+objectivesManager.objectives.Count.ToString());
            objDisplayFactory.DestroyAllObjs();
            objDisplayFactory.SetupObjs(objectivesManager?.objectives);
        }
    }
    
}

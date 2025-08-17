using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tanchiki.GameManagers;

namespace Tanchiki.Navigation {
    public class EndScreenShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Button nextLevelButton;

        private void Start()
        {
            bool isVictory = GameManager.Instance.currentState == GameManager.GameState.Victory;
            if (isVictory) 
            {
                title.text = "Victory!";
                nextLevelButton.gameObject.SetActive(true);
            }

        }
    }
}
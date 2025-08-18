using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tanchiki.GameManagers;

namespace Tanchiki.Navigation {
    public class EndScreenShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private GameObject screen;

        
        public void ShowEndScreen(bool isVictory)
        {
            screen.SetActive(true);
            if (isVictory) 
            {
                title.text = "Victory!";
                nextLevelButton.gameObject.SetActive(true);
            }
        }
    }
}
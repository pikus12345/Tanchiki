
using System.Collections.Generic;
using Tanchiki.GameManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Tanchiki.Navigation
{
    public class LevelShower : MonoBehaviour
    {
        public List<Level> Levels = new List<Level>();
        public GameObject ButtonPrefab;
        public Transform SpawnTransform;
        private List<GameObject> initializedButtons = new List<GameObject>();

        private void OnEnable()
        {
            RefreshButtons();
        }
        private void RefreshButtons()
        {
            int completedLevels = YG2.saves.completedLevels;
            foreach (var level in Levels)
            {
                Transform button = Instantiate(ButtonPrefab, SpawnTransform).transform;
                button.GetComponentInChildren<TMP_Text>()?.SetText($"{level.LevelName}\n{level.DisplayLevelIndex}");
                button.GetComponent<Button>().onClick.AddListener(
                    () => LevelManager.StartLevel(
                        level.LevelBuildIndex, 
                        level.DisplayLevelIndex)
                    );
                button.Find("Icon").GetComponent<Image>().sprite = level.LevelIcon;
                if (level.DisplayLevelIndex > completedLevels+1)
                {
                    button.GetComponent<Button>().interactable = false;
                } 
                button.SetAsFirstSibling();
                initializedButtons.Add(button.gameObject);
            }
        }
        private void OnDisable()
        {
            foreach (var btn in initializedButtons)
            {
                Destroy(btn);
            }
        }
    }
}


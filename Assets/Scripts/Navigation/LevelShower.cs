
using System.Collections.Generic;
using Tanchiki.GameManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanchiki.Navigation
{
    public class LevelShower : MonoBehaviour
    {
        public List<Level> Levels = new List<Level>();
        public GameObject ButtonPrefab;
        public Transform SpawnTransform;

        private void Start()
        {
            RefreshButtons();
        }
        private void RefreshButtons()
        {
            foreach (var level in Levels)
            {
                Transform button = Instantiate(ButtonPrefab, SpawnTransform).transform;
                button.GetComponentInChildren<TMP_Text>()?.SetText($"{level.LevelName} : {level.DisplayLevelIndex}");
                button.GetComponent<Button>().onClick.AddListener(
                    () => LevelManager.StartLevel(
                        level.LevelBuildIndex, 
                        level.DisplayLevelIndex)
                    );
                button.SetAsFirstSibling();
            }
        }
    }
}


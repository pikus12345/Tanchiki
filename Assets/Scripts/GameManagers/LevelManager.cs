using Tanchiki.Navigation;
using UnityEngine;
using YG;

namespace Tanchiki.GameManagers
{
    public static class LevelManager
    {
        private static int currentDisplayIndex; //displayLevelIndex is saving here
        private static int currentSceneIndex;
        internal static void SetCurrentLevel(int sceneIndex, int displayIndex)
        {
            currentSceneIndex = sceneIndex;
            currentDisplayIndex = displayIndex;
        }
        internal static void CompleteLevel()
        {
            if (YG2.saves.completedLevels < currentDisplayIndex)
            {
                YG2.saves.completedLevels = currentDisplayIndex;
                YG2.SaveProgress();
            }
        }
        internal static void ResetProgress()
        {
            YG2.saves.completedLevels = 0;
            YG2.SaveProgress();
        }
        internal static void StartLevel(int sceneIndex, int displayIndex)
        {
            SetCurrentLevel(sceneIndex, displayIndex);
            LoadingScreen.Instance.Loading(sceneIndex);
        }
        internal static void RestartLevel()
        {
            StartLevel(currentSceneIndex, currentDisplayIndex);
        }
        internal static void LoadNextLevel() 
        {
            StartLevel(currentSceneIndex+1, currentDisplayIndex+1);
        }
    }
}

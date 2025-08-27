using Tanchiki.Navigation;
using UnityEngine;
using YG;

namespace Tanchiki.GameManagers
{
    public static class LevelManager
    {
        private static int currentLevel;
        internal static void SetCurrentLevel(int level)
        {
            currentLevel = level;
        }
        internal static void CompleteLevel()
        {
            if (YG2.saves.completedLevels < currentLevel)
            {
                YG2.saves.completedLevels = currentLevel;
                YG2.SaveProgress();
            }
        }
        internal static void ResetProgress()
        {
            YG2.saves.completedLevels = 0;
            YG2.SaveProgress();
        }
        internal static void StartLevel(int sceneBuildIndex, int displayLevelIndex)
        {
            SetCurrentLevel(displayLevelIndex);
            LoadingScreen.Instance.Loading(sceneBuildIndex);
        }
    }
}

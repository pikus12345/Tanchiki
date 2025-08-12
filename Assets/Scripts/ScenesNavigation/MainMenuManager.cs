using UnityEngine;
using UnityEngine.SceneManagement;


namespace Tanchiki.Navigation
{
    public class MainMenuManager : MonoBehaviour
    {
        private const int TEMPLATELEVEL_BUILD_INDEX = 1;
        public void StartGame(int levelBuildIndex) {
            LoadingScreen.Instance.Loading(levelBuildIndex);
            //SceneManager.LoadScene(levelBuildIndex);
        }
        public void StartTemplateLevel()
        {
            StartGame(TEMPLATELEVEL_BUILD_INDEX);
        }
        public void Quit()
        {
            Application.Quit();
        }
        #region Навигация между панелями

        [SerializeField] GameObject mainMenuPanel;
        [SerializeField] GameObject settingsPanel;
        [SerializeField] GameObject levelSelectPanel;

        public void OpenLevelSelect()
        {
            levelSelectPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
        public void OpenSettings()
        {
            settingsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            levelSelectPanel.SetActive(false);
        }
        public void OpenMainMenu()
        {
            mainMenuPanel.SetActive(true);
            settingsPanel.SetActive(false);
            levelSelectPanel.SetActive(false);
        }
        #endregion
    }
}
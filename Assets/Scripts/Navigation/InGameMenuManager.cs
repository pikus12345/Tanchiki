using Tanchiki.GameManagers;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Tanchiki.Navigation
{
    public class InGameMenuManager : MonoBehaviour
    {
        private const int MAINMENU_BUILD_INDEX = 0;
        [SerializeField] GameObject mobileControlsObject;
        public void ReturnToMainMenu()
        {
            //LoadingScreen.Instance.Loading(MAINMENU_BUILD_INDEX);
            SceneManager.LoadScene(MAINMENU_BUILD_INDEX);
        }
        public void RestartLevel()
        {
            LevelManager.RestartLevel();
        }
        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            if (YG2.envir.isMobile)
            {
                mobileControlsObject.SetActive(true);
            }
        }

    }
}
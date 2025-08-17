using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanchiki.Navigation
{
    public class EndScreenActions : MonoBehaviour
    {
        private const int MAINMENU_BUILD_INDEX = 0;
        public void LoadMenu()
        {
            //LoadingScreen.Instance.Loading(MAINMENU_BUILD_INDEX);
            SceneManager.LoadScene(MAINMENU_BUILD_INDEX);
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadNextLevel()
        {

        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanchiki.Navigation
{
    public class InGameMenuManager : MonoBehaviour
    {
        private const int MAINMENU_BUILD_INDEX = 0;
        public void ReturnToMainMenu()
        {
            LoadingScreen.Instance.Loading(MAINMENU_BUILD_INDEX);
        }
    }
}
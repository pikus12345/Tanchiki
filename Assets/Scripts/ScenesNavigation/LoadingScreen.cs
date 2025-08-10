using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Tanchiki.Navigation
{
    public class LoadingScreen : MonoBehaviour
    {
        public GameObject screen;
        public Image scaleImage;

        public static LoadingScreen Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Loading(int sceneBuildIndex)
        {
            screen.SetActive(true);
            StartCoroutine(LoadAsync(sceneBuildIndex));
        }
        IEnumerator LoadAsync(int sceneBuildIndex)
        {
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneBuildIndex);
            loadAsync.allowSceneActivation = false;

            while (!loadAsync.isDone)
            {
                scaleImage.fillAmount = loadAsync.progress;

                if (loadAsync.progress >= .99f && !loadAsync.allowSceneActivation)
                {
                    loadAsync.allowSceneActivation = true;
                    yield return new WaitForSeconds(0.1f);
                    screen.SetActive(false);
                }
                yield return null;
            }
            
        }
    }
}
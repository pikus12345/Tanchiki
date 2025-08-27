using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tanchiki.GameManagers;


namespace Tanchiki.Navigation
{
    public class LoadingScreen : MonoBehaviour
    {
        public GameObject screen;
        public Slider scaleSlider;

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
                scaleSlider.value = loadAsync.progress;
                
                if (loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
                {
                    loadAsync.allowSceneActivation = true;
                    screen.SetActive(false);
                }
                yield return null;
            }
            
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tanchiki.GameManagers;
using TMPro;


namespace Tanchiki.Navigation
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] GameObject screen;
        [SerializeField] TMP_Text MessageText;

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
        private void ShowMessage(string message)
        {
            MessageText.text = message;
        }
        public void Loading(int sceneBuildIndex)
        {
            StartCoroutine(LoadAsync(sceneBuildIndex));
        }
        IEnumerator LoadAsync(int sceneBuildIndex)
        {
            screen.SetActive(true);
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            Debug.Log("Level loading is started");
            ShowMessage("Loading...");
            yield return new WaitUntil(() => loadAsync.isDone);
            yield return new WaitForSeconds(0.2f);
            ShowMessage("Loading is done");
            yield return new WaitForSeconds(1f);
            screen.SetActive(false);
        }
    }
}
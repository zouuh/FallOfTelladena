/*
 * Authors : Zoé
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;
    public string actualSceneName;
    public GameObject loadingScreen;
    public Slider slider;
    public void OnTriggerEnter()
    {
        FindObjectOfType<SpawnPoints>().SetPreviousPlace(actualSceneName);
        StartCoroutine(LoadAsynchronously(nextSceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}

//ZOE

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    // Public attributes
    public string nextSceneName;
    public string actualSceneName;
    public GameObject loadingScreen;
    public Slider slider;
    
    // Change scene if trigger
    public void OnTriggerEnter() {
        // Save player and PNJ before changing scene
        FindObjectOfType<Player>().SavePlayer();
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character pnj in characters) {
            pnj.SaveCharacter();
        }
        
        FindObjectOfType<SpawnPoints>().SetPreviousPlace(actualSceneName);
        StartCoroutine(LoadAsynchronously(nextSceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName) {
        // Load asynchronously to get loading screen 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        // Increase progress value of load
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}

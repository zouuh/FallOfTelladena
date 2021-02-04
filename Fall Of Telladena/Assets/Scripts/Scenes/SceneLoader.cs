﻿/*
 * Authors : Zoé, Manon
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

    public void Start() {
        loadingScreen = GameObject.FindGameObjectWithTag("Interface").transform.Find("LoadingScreen").gameObject;
        slider = loadingScreen.GetComponentInChildren<Slider>();
        Debug.Log(slider);
    }

    public void OnTriggerEnter()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositionManager>().SetPreviousPlace(actualSceneName);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        //FindObjectOfType<SpawnPoints>().SetPreviousPlace(actualSceneName);
        StartCoroutine(LoadAsynchronously(nextSceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)    {
        //Debug.Log("Loading = " + sceneName);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}

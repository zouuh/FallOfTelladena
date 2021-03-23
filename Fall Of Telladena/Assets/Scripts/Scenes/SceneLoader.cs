﻿/*
 * Authors : Zoé, Manon
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;
    public string actualSceneName;
    public GameObject loadingScreen;
    public Slider slider;
    CinemachineBrain mainCam;
    GameObject player;

    [SerializeField]
    bool loadOnTrigger = true;

    public void Start() {
        loadingScreen = GameObject.FindGameObjectWithTag("Interface").transform.Find("LoadingScreen").gameObject;
        slider = loadingScreen.GetComponentInChildren<Slider>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (loadOnTrigger && other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
            DontDestroyOnLoad(other.gameObject);
            StartLoadScene();
        }
    }

    public void StartLoadScene()
    {
        NPC[] characters = FindObjectsOfType<NPC>();
        foreach (NPC pnj in characters)
        {
            Debug.Log("saved " + pnj.name);
            pnj.SaveNPC();
        }
        player.GetComponent<PlayerPositionManager>().SetPreviousPlace(actualSceneName);
        player.GetComponent<PlayerMovement>().dust.Stop();
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        //FindObjectOfType<SpawnPoints>().SetPreviousPlace(actualSceneName);
        StartCoroutine(LoadAsynchronously(nextSceneName));
    }

    public void StartNewGame() {
        
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
            // Debug.Log(slider.value);
            if(slider.value == 1) {
                loadingScreen.SetActive(false);
            }
            yield return null;
        }
    }
}

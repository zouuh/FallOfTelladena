/*
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
    [SerializeField]
    CinemachineBrain mainCam;

    public void Start() {
        loadingScreen = GameObject.FindGameObjectWithTag("Interface").transform.Find("LoadingScreen").gameObject;
        slider = loadingScreen.GetComponentInChildren<Slider>();
    }

    public void OnTriggerEnter()
    {
        NPC[] characters = FindObjectsOfType<NPC>();
        foreach (NPC pnj in characters) {
            pnj.SaveNPC();
        }
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
            Debug.Log(slider.value);
            if(slider.value == 1) {
                loadingScreen.SetActive(false);
            }
            yield return null;
        }
    }
}

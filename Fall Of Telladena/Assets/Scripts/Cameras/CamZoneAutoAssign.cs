/*
 * Authors : Manon
 */

using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CamZoneAutoAssign : MonoBehaviour
{
    [SerializeField]
    string tagToAssign = "CamMain";

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("changing for cam main");
        GetComponent<CamZone>().virtualCamera = GameObject.FindGameObjectWithTag(tagToAssign).GetComponent<CinemachineVirtualCamera>();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

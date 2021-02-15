using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class AutoAssignCamMain : MonoBehaviour
{
    [SerializeField]
    CinemachineBrain mainCamera;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
}

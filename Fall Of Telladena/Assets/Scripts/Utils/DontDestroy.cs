/*
 * Authors : (Unity), Manon, Zoé
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(this.gameObject.tag);

        if (objs.Length > 1)
        {
            if (this.gameObject.CompareTag("Player"))
            {
                this.gameObject.SetActive(false);
            }
            if(this.gameObject != null) {
                Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(this.gameObject);

        if (this.gameObject.CompareTag("Player"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        if (this.gameObject.CompareTag("Characters")) {
            SceneManager.sceneLoaded += CheckNpc;
        }
    }

    //void Awake()
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositionManager>().SearchNewPosition();
    }

    void CheckNpc(Scene scene, LoadSceneMode mode) {
        foreach(NPC npc in GetComponentsInChildren<NPC>()) {
            npc.CheckScene();
        }
    }
}
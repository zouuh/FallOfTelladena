/*
 * Authors : (Unity), Manon, Zoé
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);

        if (objs.Length > 1)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
            if(this.gameObject != null) {
                Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        if (gameObject.CompareTag("Player"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        if (gameObject.CompareTag("Characters")) {
            SceneManager.sceneLoaded += CheckNpc;
        }
    }

    //void Awake()
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        player.GetComponent<PlayerPositionManager>().SearchNewPosition();
        player.GetComponent<PlayerMovement>().dust.Play();
    }

    void CheckNpc(Scene scene, LoadSceneMode mode) {
        foreach(NPC npc in GetComponentsInChildren<NPC>()) {
            npc.CheckScene();
        }
    }
}
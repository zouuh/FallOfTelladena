/*
 * Authors : (Unity), Manon
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
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        if (this.gameObject.CompareTag("Player"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    //void Awake()
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositionManager>().SearchNewPosition();
    }
}
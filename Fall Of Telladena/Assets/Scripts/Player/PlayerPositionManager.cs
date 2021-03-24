/* 
 * Authors : Manon 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    string previousPlace;
    public Vector3 lastSafePosition;
    /*
     private void Awake()
    {
        Debug.Log("Refresh player!");
        SpawnPoints[] spawnPoints = FindObjectsOfType<SpawnPoints>();
        Debug.Log(spawnPoints.Length);
        foreach (SpawnPoints spawnPoint in spawnPoints)
        {
            if (spawnPoint.isMySpawnPoint(previousPlace))
            {
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
                Debug.Log("Has changed player!");
            }
        }
    }
    */

    public void SetPreviousPlace(string place)
    {
        previousPlace = place;
    }
    /*
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    */
    public void SearchNewPosition()
    {
        SpawnPoints[] spawnPoints = FindObjectsOfType<SpawnPoints>();
        //Debug.Log(spawnPoints.Length);
        foreach (SpawnPoints spawnPoint in spawnPoints)
        {
            if (spawnPoint.IsMySpawnPoint(previousPlace))
            {
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
                lastSafePosition = spawnPoint.transform.position;
                /*
                StartCoroutine(wait(spawnPoint));
                return;
                */
            }
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;
    }
    /*
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Refresh player!");
        SpawnPoints[] spawnPoints = FindObjectsOfType<SpawnPoints>();
        Debug.Log(spawnPoints.Length);
        foreach (SpawnPoints spawnPoint in spawnPoints)
        {
            if (spawnPoint.isMySpawnPoint(previousPlace))
            {
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
                Debug.Log("Has changed player!" + transform.position);
                
                //StartCoroutine(wait(spawnPoint));
                //return;
                
            }
        }
    }
*/
    private void Start()
    {
        InvokeRepeating("SaveLastPosition", 0f, 10f);
    }

    public void SaveLastPosition()
    {
        lastSafePosition = transform.position;
    }

    public void SaveLastPositionLoop()
    {
        if (this.gameObject.GetComponent<CharacterController>().isGrounded)
        {
            lastSafePosition = transform.position;
        }
    }
}

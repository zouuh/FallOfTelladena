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

    private void Update()
    {
        //Debug.Log("Debug : " + transform.position);
    }
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
        Debug.Log("Refresh player!");
        SpawnPoints[] spawnPoints = FindObjectsOfType<SpawnPoints>();
        //Debug.Log(spawnPoints.Length);
        foreach (SpawnPoints spawnPoint in spawnPoints)
        {
            if (spawnPoint.isMySpawnPoint(previousPlace))
            {
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
                Debug.Log("Has changed player!" + transform.position);
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

    IEnumerator wait(SpawnPoints spawnPoint)
    {
        transform.position = spawnPoint.transform.position;
        transform.rotation = spawnPoint.transform.rotation;
        Debug.Log("Has changed player!" + transform.position);
        yield return new WaitForSeconds(.1f);
    }

    private void Start()
    {
        Debug.Log("Current pos : "+transform.position);
    }
}

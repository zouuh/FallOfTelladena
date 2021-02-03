using System.Collections;
using System.Collections.Generic;
/*
 * Authors : Zoé, Manon
 */

using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SpawnPoints : MonoBehaviour
{
    /*
    static string previousPlace;
    public string toCompare;
    void Start()
    {
        //Debug.Log("actually loading = " + SceneManager.GetActiveScene().name);
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        //CinemachineVirtualCamera virtualCameraMain = GameObject.FindGameObjectWithTag("CamMain").gameObject.GetComponent<CinemachineVirtualCamera>();
        if (previousPlace == toCompare) {
            //Debug.Log("loading from "+previousPlace);
            player.transform.position = transform.position;
            //Debug.Log("Player position = " + transform.position);
            //Debug.Log("Player position = " + player.position);
            player.transform.rotation = transform.rotation;
            //player.GetComponent<PlayerMovement>().enabled = true;

            //virtualCameraMain.transform.eulerAngles = new Vector3(30, virtualCameraMain.m_Follow.rotation.eulerAngles.y, 0);
        } 
    }
    
    public void SetPreviousPlace(string place) {
        previousPlace = place;
    }
    */
    
    public string toCompare;

    public bool isMySpawnPoint(string previousPlace)
    {
        return (toCompare == previousPlace);
    }
    
}

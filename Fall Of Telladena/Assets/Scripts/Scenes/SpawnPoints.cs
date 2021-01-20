using System.Collections;
using System.Collections.Generic;
/*
 * Authors : Zoé, Manon
 */

using UnityEngine;
using Cinemachine;

public class SpawnPoints : MonoBehaviour
{
    static string previousPlace;
    public string toCompare;
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        CinemachineVirtualCamera virtualCameraMain = GameObject.FindGameObjectWithTag("CamMain").gameObject.GetComponent<CinemachineVirtualCamera>();
        if (previousPlace == toCompare) {
            player.position = transform.position;
            player.rotation = transform.rotation;

            virtualCameraMain.transform.eulerAngles = new Vector3(30, virtualCameraMain.m_Follow.rotation.eulerAngles.y, 0);
        } 
    }
    
    public void SetPreviousPlace(string place) {
        previousPlace = place;
    }
}

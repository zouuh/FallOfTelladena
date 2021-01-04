using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    static string previousPlace;
    public string toCompare;
    public Transform player;
    void Start() {
        if (previousPlace == toCompare) {
            player.position = transform.position;
            player.rotation = transform.rotation;
        } 
    }
    
    public void SetPreviousPlace(string place) {
        previousPlace = place;
    }
}

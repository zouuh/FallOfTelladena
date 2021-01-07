//ZOE 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {
    // Static attributes
    static string previousPlace;
    // Public attributes
    public string toCompare;
    public Transform player;

    void Start() {
        // Set up player position if comme from this door
        if (previousPlace == toCompare) {
            player.position = transform.position;
            player.rotation = transform.rotation;
        } 
    }
    
    public void SetPreviousPlace(string place) {
        // Update previous place for this place
        previousPlace = place;
    }
}

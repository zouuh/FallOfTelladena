//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    //  Attributes
    public int key;
    public float[] position;

    // How to save it from Player class
    public PlayerData (Player player) {
        key = player.key;
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}

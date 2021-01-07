//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {
    // Attributes
    public int dialogueId;
    public float[] position;

    //How to save it from Character class
    public CharacterData (Character character) {
        dialogueId = character.GetDialogueId();
        
        position = new float[3];
        position[0] = character.transform.position.x;
        position[1] = character.transform.position.y;
        position[2] = character.transform.position.z;
    }
}

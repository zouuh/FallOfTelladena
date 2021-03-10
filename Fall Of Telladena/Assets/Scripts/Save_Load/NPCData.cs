//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData {
    // Attributes
    public int dialogueId;
    public string scene;
    public float[] position;

    //How to save it from NPC class
    public NPCData (NPC npc) {
        dialogueId = npc.GetDialogueId();
        scene = npc.GetScene();
        
        position = new float[3];
        position[0] = npc.transform.position.x;
        position[1] = npc.transform.position.y;
        position[2] = npc.transform.position.z;
    }
}

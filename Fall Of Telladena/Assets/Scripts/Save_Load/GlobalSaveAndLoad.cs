using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSaveAndLoad : MonoBehaviour {
    NPC[] allNpc;
    void Start() {
        allNpc = Resources.FindObjectsOfTypeAll<NPC>();
    }

    public void SaveGame() {
        foreach(NPC npc in allNpc) {
            npc.SaveNPC();
        }
        //Same pour l'inventaire et les items et le storyManager
    }

    public void LoadGame() {
        foreach(NPC npc in allNpc) {
            npc.LoadNPC();
        }
    }

    public void ResetGame() {
        foreach(NPC npc in allNpc) {
            npc.SetScene(npc.initialScene);
            npc.SetPosition(npc.initialPosition);
        }
    }
}

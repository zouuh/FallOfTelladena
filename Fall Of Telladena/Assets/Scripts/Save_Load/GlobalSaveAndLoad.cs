using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSaveAndLoad : MonoBehaviour {
    StoryManager story;
    NPC[] allNpc;
    void Start() {
        story = FindObjectOfType<StoryManager>();
        allNpc = Resources.FindObjectsOfTypeAll<NPC>();
    }

    public void SaveGame() {
        story.SaveStory();
        foreach(NPC npc in allNpc) {
            npc.SaveNPC();
        }
        //Same pour l'inventaire et les items et le storyManager
    }

    public void LoadGame() {
        story.LoadStory();
        foreach(NPC npc in allNpc) {
            npc.LoadNPC();
        }
    }

    public void ResetGame() {
        story.ResetStory();
        foreach(NPC npc in allNpc) {
            npc.ResetNPC();
            // npc.SetScene(npc.initialScene);
            // npc.SetPosition(npc.initialPosition);
        }
        SaveGame();
    }
}

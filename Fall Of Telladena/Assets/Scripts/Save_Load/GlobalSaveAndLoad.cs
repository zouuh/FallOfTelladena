using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSaveAndLoad : MonoBehaviour {
    private StoryManager story;
    private Player player;
    private NPC[] allNpc;
    void Start() {
        story = FindObjectOfType<StoryManager>();
        player = FindObjectOfType<Player>();
        allNpc = Resources.FindObjectsOfTypeAll<NPC>();
    }

    public void SaveGame() {
        story.SaveStory();
        foreach(NPC npc in allNpc) {
            npc.SaveNPC();
        }
        player.SavePlayer();

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

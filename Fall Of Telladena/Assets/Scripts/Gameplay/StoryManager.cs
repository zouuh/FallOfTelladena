/* 
 * Authors : Zoé 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour {

    // PUBLIC ATTRIBUTES
    public bool beginStoneQuests = false;
    public int mainQuestAdvencement = 0;
    public int serenityQuestAdvencement = 0;
    public int currentSerenityAdvencement = 0;
    public int currentMainAdvencement = 0;
    public int inCrystalRoom = 0;
    public int builtIrrigation = 0;
    public bool possiIsBack = false;
    public bool inBimbopCave = false;
    public NPC aiki, byoldal, gwang, migwa, namou, yoh;

    // PRIVATE ATTRIBUTES
    // TO DO : change attributes to private and add getters and setters

    // SERIALIZED ATTRIBUTES
    [SerializeField]
    public Item turbull, mazeKey, lullubyMushroom, lullubyPotion, serenityStone;

    // METHODS
    
    // TO DO : udate state machine to go to the right state if the game has been saved at a special state
    public void SetMainAdvencement(int newId) {
        if(newId > mainQuestAdvencement) {
            mainQuestAdvencement = newId;
        }
        else {
            currentMainAdvencement = newId;
        }
    }

    // TO DO : same as "SetMainAdvencement(newId)"
    public void SetSerenityAdvencement(int newId) {
        if(newId > serenityQuestAdvencement) {
            serenityQuestAdvencement = newId;
        }
        else {
            currentSerenityAdvencement = newId;
        }
    }

    // UNITY FUNCTIONS
    void Start() {
        LoadStory();
        NPC[] characters = FindObjectsOfType<NPC>();
        foreach (NPC pnj in characters) {
            switch (pnj.name) {
                case "Aïki":
                    aiki = pnj;
                    break;
                case "Byoldal":
                    byoldal = pnj;
                    break;
                case "Gwang":
                    gwang = pnj;
                    break;
                case "Migwa":
                    migwa = pnj;
                    break;
                case "Namou":
                    namou = pnj;
                    break;
                case "Yoh":
                    yoh = pnj;
                    break;
                default :
                    break;
            }
        }
    }
    
    // SAVE AND LOAD FUNCTIONS
    public void SaveStory() {
        SaveSystem.SaveStory(this);
    }
    
    // Read save file and update attributes
    public void LoadStory() {
        StoryData data = SaveSystem.LoadStory();

        inCrystalRoom = data.inCrystalRoom;
        builtIrrigation = data.builtIrrigation;
        possiIsBack = data.possiIsBack;
        inBimbopCave = data.inBimbopCave;
        beginStoneQuests = data.beginStoneQuests;
        mainQuestAdvencement = data.mainQuestAdvencement;
        serenityQuestAdvencement = data.serenityQuestAdvencement;
    }

    public void ResetStory() {
        inCrystalRoom = 0;
        builtIrrigation = 0;
        possiIsBack = false;
        inBimbopCave = false;
        beginStoneQuests = false;
        mainQuestAdvencement = 0;
        serenityQuestAdvencement = 0;
        SaveStory();
    }
}

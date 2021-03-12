/* 
 * Authors : Zoé 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour {
    // PUBLIC ATTRIBUTES
    public int inCrystalRoom = 0;
    public int builtIrrigation = 0;
    public bool possiIsBack = false;
    public bool inBimbopCave = false;
    public bool beginStoneQuests = false;
    public int mainQuestAdvencement = 0;
    public int serenityQuestAdvencement = 0;
    private int currentSerenityAdvencement = 0;
    private int currentMainAdvencement = 0;

    // PRIVATE ATTRIBUTES
    public NPC aiki, byoldal, gwang, migwa, namou, yoh;

    // SERIALIZED ATTRIBUTES
    [SerializeField]
    public Item turbull, mazeKey, lullubyMushroom, lullubyPotion, serenityStone;

    // Methods
    public void SetMainAdvencement(int newId) {
        if(newId > mainQuestAdvencement) {
            mainQuestAdvencement = newId;
        }
        else {
            currentMainAdvencement = newId;
        }
    }

    public void SetSerenityAdvencement(int newId) {
        if(newId > serenityQuestAdvencement) {
            serenityQuestAdvencement = newId;
        }
        else {
            currentSerenityAdvencement = newId;
        }
    }


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
    
    // Save and load functions
    public void SaveStory() {
        Debug.Log("Save story");
        SaveSystem.SaveStory(this);
    }
    
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
    }
}

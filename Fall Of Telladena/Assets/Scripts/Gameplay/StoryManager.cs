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

    // void Update() {
    //     if(inCrystalRoom == 1) {
    //         // The story begin 
    //         inCrystalRoom++;
    //         SaveStory();
    //         // Aiki moves
    //         aiki.SetScene("OutsideCastle");
    //         aiki.SetPosition(new Vector3(-20f,-10.138f, -10f));
    //         aiki.SetDialogueID(1);
    //         aiki.SaveNPC();
    //         // _________ Lancer la cinématique

    //         // Important players appears
    //         yoh.SetScene("Village");
    //         yoh.SaveNPC();
    //     }
    //     if(aiki.HaveSeenDialogue(1)) {
    //         aiki.SetDialogueID(2);
    //         aiki.SaveNPC();
    //     }

    //     /////////////////////// SERENITY QUEST ///////////////////////
    //     if(yoh.HaveSeenDialogue(0) && inCrystalRoom >= 1) {
    //         // Oksusu need a Turbull
    //         yoh.SetDialogueID(1);
    //         yoh.SaveNPC();
    //     }
    //     if(yoh.HaveSeenDialogue(1)) {
    //         // Gwang can give Oksusu a Turbull
    //         gwang.SetDialogueID(1);
    //         gwang.SaveNPC();
    //     }
    //     if(gwang.HaveSeenDialogue(1)) {
    //         // Add a turbull in the inventory
    //         Debug.Log("give Turbull");
    //         Inventory.instance.Add(turbull);
    //         gwang.SetDialogueID(2);
    //         gwang.SaveNPC();
    //     }
    //     if(yoh.HaveSeenDialogue(1) && Inventory.instance.HasTool("Turbull", 1)) {
    //         // Oksusu give the Turbull to Yoh and have to find the Luluby mushrooms and Migwa have the first labyrinthe key
    //         yoh.SetDialogueID(2);
    //         yoh.SaveNPC();
    //         namou.SetDialogueID(1);
    //         namou.SaveNPC();
    //     }
    //     if(yoh.HaveSeenDialogue(2)) {
    //         // Yoh want the Luluby mushroom
    //         Inventory.instance.Remove(turbull);
    //         yoh.SetDialogueID(3);
    //         yoh.SaveNPC();
    //         migwa.SetDialogueID(1);
    //         migwa.SaveNPC();
    //     }
    //     if(migwa.HaveSeenDialogue(1)) {
    //         // Migwa give the first key and Byoldal have the second one
    //         Inventory.instance.Add(mazeKey);
    //         byoldal.SetDialogueID(1);
    //         byoldal.SaveNPC();
    //         // migwa.SetDialogueID(2);
    //         // migwa.SaveNPC();
    //     }
    //     if(byoldal.HaveSeenDialogue(1)) {
    //         Inventory.instance.Add(mazeKey);
    //         byoldal.SetDialogueID(2);
    //         byoldal.SaveNPC();
    //     }
    //     if(Inventory.instance.HasTool("LullubyMushroom", 1)) {
    //         lulluby += 1;
    //     }
    //     if (lulluby == 1) {
    //         // Yoh want something to drink and Migwa can create the potion
    //         namou.SetDialogueID(0);
    //         namou.SaveNPC();
    //         yoh.SetDialogueID(4);
    //         yoh.SaveNPC();
    //         migwa.SetDialogueID(3);
    //         migwa.SaveNPC();
    //         lulluby += 1;
    //     }
    //     if(migwa.HaveSeenDialogue(3)) {
    //         // Yoh accept the potion 
    //         Inventory.instance.Remove(lullubyMushroom);
    //         Inventory.instance.Add(lullubyPotion);
    //         yoh.SetDialogueID(5);
    //         yoh.SaveNPC();
    //         migwa.SetDialogueID(4);
    //         migwa.SaveNPC();
    //     }
    //     if(yoh.HaveSeenDialogue(5)) {
    //         // Yoh give the stone to Oksusu
    //         Inventory.instance.Add(serenityStone);
    //         yoh.SetDialogueID(6);
    //         yoh.SaveNPC();
    //     }
    // }






    
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

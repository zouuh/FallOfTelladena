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

    // PRIVATE ATTRIBUTES
    public NPC aiki, byoldal, gwang, migwa, yoh;

    // SERIALIZED ATTRIBUTES
    [SerializeField]
    Item turbull, mazeKey, lullubyMushroom, lullubyPotion, serenityStone;


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
                case "Yoh":
                    yoh = pnj;
                    break;
                default :
                    break;
            }
        }
    }

    void Update() {
        if(inCrystalRoom == 1) {
            // The story begin 
            Debug.Log("in crystal Room");
            inCrystalRoom++;
            SaveStory();
            // Aiki moves
            aiki.SetScene("OutsideCastle");
            aiki.SetPosition(new Vector3(-20f,-10.138f, -10f));
            aiki.SetDialogueID(1);
            aiki.SaveNPC();
            // _________ Lancer la cinématique

            // Important players appears
            yoh.SetScene("Village");
            yoh.SaveNPC();
        }
        if(aiki.HaveSeenDialogue(1)) {
            aiki.SetDialogueID(2);
            aiki.SaveNPC();
        }

        /////////////////////// SERENITY QUEST ///////////////////////
        if(yoh.HaveSeenDialogue(0) && inCrystalRoom >= 1) {
            // Oksusu need a Turbull
            yoh.SetDialogueID(1);
            yoh.SaveNPC();
        }
        if(yoh.HaveSeenDialogue(1)) {
            // Gwang can give Oksusu a Turbull
            gwang.SetDialogueID(1);
            gwang.SaveNPC();
        }
        if(gwang.HaveSeenDialogue(1)) {
            // Add a turbull in the inventory
            Debug.Log("give Turbull");
            Inventory.instance.Add(turbull);
            gwang.SetDialogueID(2);
            gwang.SaveNPC();
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
    }

    public void ResetStory() {
        inCrystalRoom = 0;
        builtIrrigation = 0;
        possiIsBack = false;
        inBimbopCave = false;
    }
}

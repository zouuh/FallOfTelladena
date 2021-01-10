//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1 - POUR FAIRE QUELQUE CHOSE SUR UN PERSO EN PARTICULIER TROUVER UNE FONCTION TYPE "FIND BY NAME"
// 2 - POUR SAVOIR SI UN DIALOGUE A DEJA ETE LU, FAIRE UNE FONCTION "HAVESEEN(INT)" QUI VERIFIERA 
//          SI LE DIALOGUEID EST LE MEME ET SI LE PARAMETRE "HASSEEN" EST TRUE
// 3 - FAIRE UNE FONCTION POUR SAVOIR SI UN ITEM EST DANS L'INVENTAIRE



public class StoryManager : MonoBehaviour
{
    public int inCrystalRoom = 0;
    public int yohDialogue = 0;
    public int migwaDialogue = 0;

    public bool hasTurbull = false; // Create function to check by name if something is in the inventory
    public bool hasLuluby = false; // Create function to check by name if something is in the inventory
    Character[] characters;

    void Start() {
        characters = FindObjectsOfType<Character>();
    }
    void Update() {
        if(inCrystalRoom == 1) {
            // The story begin

            //_____________________________ Change dialogue d'autres persos
            // Move Aïki and change his dialogue
            foreach (Character pnj in characters) { //_______________________MODIFIER CA POUR UN TRUC GENRE FIND OBJECT BY NAME
                if (pnj.name == "Aïki") {
                    pnj.SetScene("OutsideCastle");
                    pnj.SetDialogueID(1);
                    pnj.SetPosition(new Vector3(-20f, -10.1379f, -10f));
                }
            }
        }
        if(yohDialogue == 1 && inCrystalRoom >= 1) {
            // Oksusu need a Turbull
            ChangeDialogueOf(1, "Yoh");
        }
        if(yohDialogue == 2) {
            // Petit Dadet can give Oksusu a Turbull
            ChangeDialogueOf(1, "PetitDadet");
        }
        if(yohDialogue >= 2 && hasTurbull) {
            // Oksusu give the Turbull to Yoh and have to find the Luluby mushrooms and Migwa have the first labyrinthe key
            ChangeDialogueOf(2, "Yoh");
            //___________________ Enlever 1 turbull de l'inventaire
            ChangeDialogueOf(1, "Pititronc");
            ChangeDialogueOf(1, "Migwa");
        }
        if(yohDialogue == 3) {
            // Yoh want the Luluby mushroom
            ChangeDialogueOf(3, "Yoh");
        }
        if(migwaDialogue == 2) {
            // Migwa give the first key and Byoldal have the second one
            // ______________ Ajouter une "clé du labyrinthe" dans l'inventaire
            ChangeDialogueOf(1, "Byoldal");
            ChangeDialogueOf(2, "Migwa");
        }
        if(hasLuluby) {
            // Yoh want something to drink and Migwa can create the potion
            ChangeDialogueOf(4, "Yoh");
            ChangeDialogueOf(3, "Migwa");
        }
        if(migwaDialogue == 4) {
            // Yoh accept the potion
            // _____________ Ajouter une potion dans l'inventaire
            // _____________ Enlever un champignon de Luluby de l'inventaire
            ChangeDialogueOf(4, "Migwa");
            ChangeDialogueOf(5, "Yoh");
        }
        if(yohDialogue == 6) {
            // _____________ Débloquer la pierre de sérénité et l'ajouter a l'inventaire
        }
    }


    void ChangeDialogueOf(int id, string name) {
        foreach (Character pnj in characters) {  //______________________MODIFIER CA POUR UN TRUC GENRE FIND OBJECT BY NAME
            if(pnj.name == name) {
                pnj.SetDialogueID(id);
            }
        }
    }
}

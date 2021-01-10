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
    public int kiyoDialogue = 0;
    public int petitDadetDialogue = 0;
    public int byoldalDialogue = 0;
    public int flamencoDialogue = 0;
    public int grandDadetDialogue = 0;
    public int manoDialogue = 0;
    public int tokiDialogue = 0;
    public int hobaDialogue = 0;
    public int noonaDialogue = 0;
    public int kogaDialogue = 0;
    public int danseuseDialogue = 0;
    public int builtIrrigation = 0;
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
        /////////////////////// SERENITY QUEST ///////////////////////
        if(yohDialogue == 1 && inCrystalRoom >= 1) {
            // Oksusu need a Turbull
            ChangeDialogueOf(1, "Yoh");
        }
        if(yohDialogue == 2) {
            // Petit Dadet can give Oksusu a Turbull
            ChangeDialogueOf(1, "PetitDadet");
        }
        if(petitDadetDialogue == 2) {
            // ______________ ajouter un turbull à l'inventaire
            ChangeDialogueOf(2, "PetitDadet");
        }
        if(yohDialogue >= 2 && HasInInventory("Turbull")) {
            // Oksusu give the Turbull to Yoh and have to find the Luluby mushrooms and Migwa have the first labyrinthe key
            ChangeDialogueOf(2, "Yoh");
            ChangeDialogueOf(1, "Pititronc");
        }
        if(yohDialogue == 3) {
            // Yoh want the Luluby mushroom
            //___________________ Enlever 1 turbull de l'inventaire
            ChangeDialogueOf(3, "Yoh");
            ChangeDialogueOf(1, "Migwa");
        }
        if(migwaDialogue == 2) {
            // Migwa give the first key and Byoldal have the second one
            // ______________ Ajouter une "clé du labyrinthe" dans l'inventaire
            ChangeDialogueOf(1, "Byoldal");
            ChangeDialogueOf(2, "Migwa");
        }
        if(byoldalDialogue == 2) {
            ChangeDialogueOf(2, "Byoldal");
        }
        if(HasInInventory("LulubyMushroom")) {
            // Yoh want something to drink and Migwa can create the potion
            ChangeDialogueOf(4, "Yoh");
            ChangeDialogueOf(3, "Migwa");
            ChangeDialogueOf(0, "Pititronc");
        }
        if(migwaDialogue == 4) {
            // Yoh accept the potion 
            // _____________ Ajouter une potion dans l'inventaire
            // _____________ Enlever un champignon de Luluby de l'inventaire
            ChangeDialogueOf(4, "Migwa");
            ChangeDialogueOf(5, "Yoh");
        }
        if(yohDialogue == 6) {
            // Yoh give the potion to Oksusu
            // _____________ Débloquer la pierre de sérénité et l'ajouter a l'inventaire
            ChangeDialogueOf(6, "Yoh");
        }

        /////////////////////// FERTILITY QUEST ///////////////////////
        if(kiyoDialogue == 2) {
            // The 4 pnj will give symbols of their places to Oksusu
            ChangeDialogueOf(1, "Mano");
            ChangeDialogueOf(1, "GrandDadet");
            ChangeDialogueOf(1, "Flamenco");
            ChangeDialogueOf(1, "Toki");
        }
        if(flamencoDialogue == 2) {
            // Oksusu have the Castle symbol
            ChangeDialogueOf(2, "Flamenco");
            // ___________ Ajouter la coronne a l'inventaire
        }
        if(grandDadetDialogue == 2) {
            // Oksusu have the Village symbol
            ChangeDialogueOf(2, "GrandDadet");
            // ___________ Ajouter l'engrenage a l'inventaire
        }
        if(manoDialogue == 2) {
            // Oksusu have the field symbol
            ChangeDialogueOf(2, "Mano");
            // ___________ Ajouter la tige de blé a l'inventaire
        }
        if(tokiDialogue == 2) {
            // Oksusu have the River symbol
            ChangeDialogueOf(2, "Toki");
            // ___________ Ajouter le poisson a l'inventaire
        }
        // __________ Quand Oksusu va dans la maison d'Hoba et voit la pierre -> changer dialogue Hoba pour 1
        if(builtIrrigation == 1) {
            if(HasInInventory("SerenityStone")) {
                // Hoba is happy
                ChangeDialogueOf(3, "Hoba");
            }
            else {
                // Hoba isn't happy, she wants a better irrigation system
                ChangeDialogueOf(2, "Hoba");
            }
        }
        if(builtIrrigation == 2) {
            // Hoba is happy
            ChangeDialogueOf(4, "Hoba");
        }
        if(hobaDialogue == 4 || hobaDialogue == 5) {
            // Hoba gives the fertility stone to Oksusu and Flamenco have a new mission for Oksusu
            // _____________ Débloquer la pierre de fertilité et l'ajouter a l'inventaire
            ChangeDialogueOf(5, "Hoba");
            ChangeDialogueOf(3, "Flamenco");
            ChangeDialogueOf(3, "Toki");
        }

        /////////////////////// CLARTY QUEST ///////////////////////
        if(kogaDialogue == 1) {
            ChangeDialogueOf(1, "Noona");
        }
        if(noonaDialogue == 2) {
            ChangeDialogueOf(1, "Koga");
        }
        if(kogaDialogue == 2) {
            ChangeDialogueOf(2, "Noona");
        }
        if(HasInInventory("Flower")) {
            // ____________ Déplacer Noona et Koga
            ChangeDialogueOf(1, "Danseuse");
        }
        if(danseuseDialogue == 2) {
            ChangeDialogueOf(1, "Halma");
        }
    }


    void ChangeDialogueOf(int id, string name) {
        foreach (Character pnj in characters) {  //______________________MODIFIER CA POUR UN TRUC GENRE FIND OBJECT BY NAME
            if(pnj.name == name) {
                pnj.SetDialogueID(id);
            }
        }
    }
    bool HasInInventory(string item) {
        // Implementer la fonction
        return false;
    }
}

//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// DONE - POUR FAIRE QUELQUE CHOSE SUR UN PERSO EN PARTICULIER TROUVER UNE FONCTION TYPE "FIND BY NAME"
// DONE - POUR SAVOIR SI UN DIALOGUE A DEJA ETE LU, FAIRE UNE FONCTION "HAVESEEN(INT)" QUI VERIFIERA 
//          SI LE DIALOGUEID EST LE MEME ET SI LE PARAMETRE "HASSEEN" EST TRUE
// 3 - FAIRE UNE FONCTION POUR SAVOIR SI UN ITEM EST DANS L'INVENTAIRE

public class StoryManager : MonoBehaviour {
    public int inCrystalRoom = 0;
    public int builtIrrigation = 0;
    public bool possiIsBack = false;
    public bool inBimbopCave = false;
    NPC aiki;
    NPC aleya;
    NPC byoldal;
    NPC eno;
    NPC gwang;
    NPC halma;
    NPC hoba;
    NPC jouma;
    NPC joya;
    NPC kiyo;
    NPC koga;
    NPC manai;
    NPC mano;
    NPC migwa;
    NPC namou;
    NPC noona;
    NPC pada;
    NPC possa;
    NPC toki;
    NPC won;
    NPC yoh;

    void Start() {
        NPC[] characters = Resources.FindObjectsOfTypeAll<NPC>();
        foreach (NPC pnj in characters) {
            switch (pnj.name) {
                case "Aïki":
                    aiki = pnj;
                    break;
                case "Aleya":
                    aleya = pnj;
                    break;
                case "Byoldal":
                    byoldal = pnj;
                    break;
                case "Eno":
                    eno = pnj;
                    break;
                case "Gwang":
                    gwang = pnj;
                    break;
                case "Halma":
                    halma = pnj;
                    break;
                case "Hoba":
                    hoba = pnj;
                    break;
                case "Jouma":
                    jouma = pnj;
                    break;
                case "Joya":
                    joya = pnj;
                    break;
                case "Koga":
                    koga = pnj;
                    break;
                case "Kiyo":
                    kiyo = pnj;
                    break;
                case "Manaï":
                    manai = pnj;
                    break;
                case "Mano":
                    mano = pnj;
                    break;
                case "Migwa":
                    migwa = pnj;
                    break;
                case "Namou":
                    namou = pnj;
                    break;
                case "Noona":
                    noona = pnj;
                    break;
                case "Pada":
                    pada = pnj;
                    break;
                case "Possa":
                    possa = pnj;
                    break;
                case "Toki":
                    toki = pnj;
                    break;
                case "Won":
                    won = pnj;
                    break;
                case "Yoh":
                    yoh = pnj;
                    break;
                default :
                    Debug.Log(pnj.name + " not assigned");
                    break;
            }
        }
    }
    void Update() {
        Debug.Log("StoryManager");
        if(inCrystalRoom == 1) {
            // The story begin
            aiki.SetScene("OutsideCastle");
            aiki.SetDialogueID(1);
            aiki.SetPosition(new Vector3(-20f, -10.1379f, -10f));
        }
        if(aiki.HaveSeenDialogue(1)) {
            aiki.SetDialogueID(2);
        }
        /////////////////////// SERENITY QUEST ///////////////////////
        if(yoh.HaveSeenDialogue(0) && inCrystalRoom >= 1) {
            // Oksusu need a Turbull
            yoh.SetDialogueID(1);
        }
        if(yoh.HaveSeenDialogue(1)) {
            // Petit Dadet can give Oksusu a Turbull
            ChangeDialogueOf(1, "Gwang");
        }
        if(gwang.HaveSeenDialogue(1)) {
            // ______________ ajouter un turbull à l'inventaire
            ChangeDialogueOf(2, "Gwang");
        }
        if(yoh.HaveSeenDialogue(1) && HasInInventory("Turbull", 1)) {
            // Oksusu give the Turbull to Yoh and have to find the Luluby mushrooms and Migwa have the first labyrinthe key
            yoh.SetDialogueID(2);
            ChangeDialogueOf(1, "Pititronc");
        }
        if(yoh.HaveSeenDialogue(2)) {
            // Yoh want the Luluby mushroom
            //___________________ Enlever 1 turbull de l'inventaire
            yoh.SetDialogueID(3);
            migwa.SetDialogueID(1);
        }
        if(migwa.HaveSeenDialogue(1)) {
            // Migwa give the first key and Byoldal have the second one
            // ______________ Ajouter une "clé du labyrinthe" dans l'inventaire
            byoldal.SetDialogueID(1);
            migwa.SetDialogueID(2);
        }
        if(byoldal.HaveSeenDialogue(1)) {
            byoldal.SetDialogueID(2);
        }
        if(HasInInventory("LulubyMushroom", 1)) {
            // Yoh want something to drink and Migwa can create the potion
            ChangeDialogueOf(0, "Pititronc");
            yoh.SetDialogueID(4);
            migwa.SetDialogueID(3);
        }
        if(migwa.HaveSeenDialogue(3)) {
            // Yoh accept the potion 
            // _____________ Ajouter une potion dans l'inventaire
            // _____________ Enlever un champignon de Luluby de l'inventaire
            yoh.SetDialogueID(5);
            migwa.SetDialogueID(4);
        }
        if(yoh.HaveSeenDialogue(5)) {
            // Yoh give the potion to Oksusu
            // _____________ Débloquer la pierre de sérénité et l'ajouter a l'inventaire
            yoh.SetDialogueID(6);
        }

        /////////////////////// FERTILITY QUEST ///////////////////////
        if(kiyo.HaveSeenDialogue(1)) {
            // The 4 pnj will give symbols of their places to Oksusu
            ChangeDialogueOf(1, "Won");
            ChangeDialogueOf(1, "Aleya");
            mano.SetDialogueID(1);
            toki.SetDialogueID(1);
        }
        if(aleya.HaveSeenDialogue(1)) {
            // Oksusu have the Castle symbol
            ChangeDialogueOf(2, "Aleya");
            // ___________ Ajouter la coronne a l'inventaire
        }
        if(won.HaveSeenDialogue(1)) {
            // Oksusu have the Village symbol
            ChangeDialogueOf(2, "Won");
            // ___________ Ajouter l'engrenage a l'inventaire
        }
        if(mano.HaveSeenDialogue(1)) {
            // Oksusu have the field symbol
            mano.SetDialogueID(2);
            // ___________ Ajouter la tige de blé a l'inventaire
        }
        if(toki.HaveSeenDialogue(1)) {
            // Oksusu have the River symbol
            toki.SetDialogueID(2);
            // ___________ Ajouter le poisson a l'inventaire
        }
        // __________ Quand Oksusu va dans la maison d'Hoba et voit la pierre -> changer dialogue Hoba pour 1
        if(builtIrrigation == 1) {
            if(HasInInventory("SerenityStone", 1)) {
                // Hoba is happy
                hoba.SetDialogueID(3);
            }
            else {
                // Hoba isn't happy, she wants a better irrigation system
                hoba.SetDialogueID(2);
            }
        }
        if(builtIrrigation == 2) {
            // Hoba is happy
            hoba.SetDialogueID(4);
        }
        if(hoba.HaveSeenDialogue(3) || hoba.HaveSeenDialogue(4)) {
            // Hoba gives the fertility stone to Oksusu and Aleya have a new mission for Oksusu
            // _____________ Débloquer la pierre de fertilité et l'ajouter a l'inventaire
            ChangeDialogueOf(3, "Aleya");
            hoba.SetDialogueID(5);
            toki.SetDialogueID(3);
            kiyo.SetDialogueID(1);
            joya.SetDialogueID(1);
        }

        /////////////////////// CLARTY QUEST ///////////////////////
        if(koga.HaveSeenDialogue(0)) {
            noona.SetDialogueID(1);
        }
        if(noona.HaveSeenDialogue(1)) {
            koga.SetDialogueID(1);
        }
        if(koga.HaveSeenDialogue(1)) {
            noona.SetDialogueID(2);
        }
        if(HasInInventory("Flower", 1)) {
            // ____________ mettre les positions de Noona et Koga
            ChangeDialogueOf(1, "Jouma");
            koga.SetScene("Forest");
            noona.SetScene("Forest");
        }
        if(jouma.HaveSeenDialogue(1)) {
            halma.SetDialogueID(1);
        }
        if(inBimbopCave) {
            koga.SetDialogueID(2);
            // ____________Lance le dialogue de Koga
        }
        if(koga.HaveSeenDialogue(2)) {
            noona.SetDialogueID(3);
            // ___________Lance le dialogue de Noona
        }
        if(noona.HaveSeenDialogue(3)) {
            koga.SetDialogueID(3);
            // ___________Lance le dialogue de Koga
        }
        if(koga.HaveSeenDialogue(3)) {
            noona.SetDialogueID(4);
            // ___________Lance le dialogue de Noona
        }
        if(noona.HaveSeenDialogue(4)) {
            if(!HasInInventory("Recipient", 1) && !HasInInventory("FertilityStone", 1)) {
                koga.SetDialogueID(4);
                // ___________Lance le dialogue de Koga
            }
            else if(!HasInInventory("Flower", 4)) {
                koga.SetDialogueID(5);
                // ___________Lance le dialogue de Koga
            }
            else {
                koga.SetDialogueID(6);
                // ___________Lance le dialogue de Koga
            }
        }
        if(inBimbopCave && (koga.HaveSeenDialogue(4) || koga.HaveSeenDialogue(5))) {
            noona.SetDialogueID(5);
            // ____________Lance le dialogue de Noona
        }
        if(inBimbopCave && koga.HaveSeenDialogue(6)) {
            noona.SetDialogueID(5);
            koga.SetDialogueID(6);
            // ______________ Enlever les 4 fleurs et le récipient si pas sérénité
            // ______________ Débloquer la pierre de clarté et l'ajouter a l'inventaire
            // ______________ Lancer le dialogue de Noona
        }
        if(noona.HaveSeenDialogue(5)) {
            ChangeDialogueOf(2, "Jouma");
            noona.SetDialogueID(6);
            halma.SetDialogueID(2);
            koga.SetScene("Village");
            noona.SetScene("Village");
            // ____________ mettre la position de Koga et Noona
        }
        if(HasInInventory("Recipient", 1) && !HasInInventory("Flower", 4)) {
            koga.SetDialogueID(5);
        }
        if(HasInInventory("Recipient",1) && HasInInventory("Flower", 4)) {
            koga.SetDialogueID(6);
        }
        if (!inBimbopCave && koga.HaveSeenDialogue(6)) {
            // ______________ Enlever les 4 fleurs et le récipient si pas sérénité
            // ______________ Débloquer la pierre de clarté et l'ajouter a l'inventaire
            koga.SetDialogueID(7);
            noona.SetDialogueID(7);
        }
        
        /////////////////////// LOST IN THE FOREST QUEST ///////////////////////
        if(possa.HaveSeenDialogue(0)) {
            possa.SetDialogueID(1);
        }
        if(possiIsBack) {
            possa.SetDialogueID(2);
        }
        if(possa.HaveSeenDialogue(2)) {
            // ___________ Ajouter une clef à l'inventaire
            possa.SetDialogueID(3);
        }     

        /////////////////////// FIND NOUNOURS QUEST ///////////////////////
        if(manai.HaveSeenDialogue(0)) {
            manai.SetDialogueID(1);
        }
        if(HasInInventory("Nounours", 1)) {
            manai.SetDialogueID(2);
        }
        if(manai.HaveSeenDialogue(2)) {
            // _____________ Ajouter une clef à l'inventaire
            // _____________ Enlever le nounours de l'inventaire
            manai.SetDialogueID(3);
        }

        /////////////////////// BEAUTIFUL DRESS QUEST ///////////////////////
        if(HasInInventory("AleyaDress", 1)) {
            ChangeDialogueOf(4, "Aleya");
        }
        if(aleya.HaveSeenDialogue(4)) {
            // ________________ Enlever la robe de l'inventaire
            // ________________ Ajouter la clef a l'inventaire 
            ChangeDialogueOf(5, "Aleya");
        }

        /////////////////////// BEAUTIFUL DRESS QUEST ///////////////////////
        


        /////////////////////// END MAIN QUEST ///////////////////////
        if(aiki.HaveSeenDialogue(2) && inCrystalRoom == -1){
            aiki.SetDialogueID(3);
        }
        if(HasInInventory("Key", 1)) {
            aiki.SetDialogueID(4);
        }
        if(HasInInventory("Key", 10)) {
            aiki.SetDialogueID(5);
        }
        if(aiki.HaveSeenDialogue(5)) {
            aiki.SetDialogueID(6);
            pada.SetDialogueID(3);
            halma.SetDialogueID(3);
            byoldal.SetDialogueID(4);
            possa.SetDialogueID(4);
            migwa.SetDialogueID(5);
            ChangeDialogueOf(3, "Jouma");
            // __________ Enlever les clefs de l'inventaire et les pierres ?
            // __________ Lancer l'animation de fin avec le discours de l'esprit de la forêt
        }
    }

    void ChangeDialogueOf(int id, string name) {
        // foreach (Character pnj in characters) {  //______________________MODIFIER CA POUR UN TRUC GENRE FIND OBJECT BY NAME
        //     if(pnj.name == name) {
        //         pnj.SetDialogueID(id);
        //     }
        // }
    }
    bool HasInInventory(string item, int nbItem) {
        // Implementer la fonction
        return false;
    }
}

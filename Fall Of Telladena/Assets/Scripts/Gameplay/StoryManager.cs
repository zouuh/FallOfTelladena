/* 
 * Authors : Zoé 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// DONE - POUR FAIRE QUELQUE CHOSE SUR UN PERSO EN PARTICULIER TROUVER UNE FONCTION TYPE "FIND BY NAME"
// DONE - POUR SAVOIR SI UN DIALOGUE A DEJA ETE LU, FAIRE UNE FONCTION "HAVESEEN(INT)" QUI VERIFIERA 
//          SI LE DIALOGUEID EST LE MEME ET SI LE PARAMETRE "HASSEEN" EST TRUE
// DONE - FAIRE UNE FONCTION POUR SAVOIR SI UN ITEM EST DANS L'INVENTAIRE

public class StoryManager : MonoBehaviour {

    // PUBLIC ATTRIBUTES
    public int inCrystalRoom = 0;
    public int builtIrrigation = 0;
    public bool possiIsBack = false;
    public bool inBimbopCave = false;

    // PRIVATE ATTRIBUTES
    NPC aiki, aleya, byoldal, eno, gwang, halma, hoba, jouma, joya, kiyo, koga, manai, mano, migwa, namou, noona, pada, possa, toki, won, yoh;
    // NPC aleya;
    // NPC byoldal;
    // NPC eno;
    // NPC gwang;
    // NPC halma;
    // NPC hoba;
    // NPC jouma;
    // NPC joya;
    // NPC kiyo;
    // NPC koga;
    // NPC manai;
    // NPC mano;
    // NPC migwa;
    // NPC namou;
    // NPC noona;
    // NPC pada;
    // NPC possa;
    // NPC toki;
    // NPC won;
    // NPC yoh;

    // SERIALIZED ATTRIBUTES
    [SerializeField]
    Item turbull, mazeKey, lullubyMushroom, lullubyPotion, serenityStone;

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
        //Debug.Log("StoryManager");
        if(inCrystalRoom == 1) {  // --OK
            // The story begin 
            aiki.SetScene("OutsideCastle");
            aiki.SetDialogueID(1);
            aiki.SetPosition(new Vector3(-20f, -10.1379f, -10f));
            aiki.SaveNPC();
        }
        if(aiki.HaveSeenDialogue(1)) {  // --OK
            aiki.SetDialogueID(2);
            
        }
        /////////////////////// SERENITY QUEST ///////////////////////
        if(yoh.HaveSeenDialogue(0) && inCrystalRoom >= 1) {
            // Oksusu need a Turbull
            yoh.SetDialogueID(1);
        }
        if(yoh.HaveSeenDialogue(1)) {
            // Petit Dadet can give Oksusu a Turbull
            gwang.SetDialogueID(1);
        }
        if(gwang.HaveSeenDialogue(1)) {
            Inventory.instance.Add(turbull);
            gwang.SetDialogueID(2);
        }
        if(yoh.HaveSeenDialogue(1) && Inventory.instance.HasTool("Turbull", 1)) {
            // Oksusu give the Turbull to Yoh and have to find the Luluby mushrooms and Migwa have the first labyrinthe key
            yoh.SetDialogueID(2);
            namou.SetDialogueID(1);
        }
        if(yoh.HaveSeenDialogue(2)) {
            // Yoh want the Luluby mushroom
            Inventory.instance.Remove(turbull);
            yoh.SetDialogueID(3);
            migwa.SetDialogueID(1);
        }
        if(migwa.HaveSeenDialogue(1)) {
            // Migwa give the first key and Byoldal have the second one
            Inventory.instance.Add(mazeKey);
            byoldal.SetDialogueID(1);
            migwa.SetDialogueID(2);
        }
        if(byoldal.HaveSeenDialogue(1)) {
            Inventory.instance.Add(mazeKey);
            byoldal.SetDialogueID(2);
        }
        if(Inventory.instance.HasTool("LulubyMushroom", 1)) {
            // Yoh want something to drink and Migwa can create the potion
            namou.SetDialogueID(0);
            yoh.SetDialogueID(4);
            migwa.SetDialogueID(3);
        }
        if(migwa.HaveSeenDialogue(3)) {
            // Yoh accept the potion 
            Inventory.instance.Remove(lullubyMushroom);
            Inventory.instance.Add(lullubyPotion);
            yoh.SetDialogueID(5);
            migwa.SetDialogueID(4);
        }
        if(yoh.HaveSeenDialogue(5)) {
            // Yoh give the potion to Oksusu
            Inventory.instance.Add(serenityStone);
            yoh.SetDialogueID(6);
        }

        /////////////////////// FERTILITY QUEST ///////////////////////
        if(kiyo.HaveSeenDialogue(1)) {
            // The 4 pnj will give symbols of their places to Oksusu
            won.SetDialogueID(1);
            aleya.SetDialogueID(1);
            mano.SetDialogueID(1);
            toki.SetDialogueID(1);
        }
        if(aleya.HaveSeenDialogue(1)) {
            // Oksusu have the Castle symbol
            aleya.SetDialogueID(2);
            // ___________ Ajouter la coronne a l'inventaire
        }
        if(won.HaveSeenDialogue(1)) {
            // Oksusu have the Village symbol
            won.SetDialogueID(2);
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
            if(Inventory.instance.HasTool("SerenityStone", 1)) {
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
            aleya.SetDialogueID(3);
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
        if(Inventory.instance.HasTool("Flower", 1)) {
            // ____________ mettre les positions de Noona et Koga
            jouma.SetDialogueID(1);
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
            if(!Inventory.instance.HasTool("Recipient", 1) && !Inventory.instance.HasTool("FertilityStone", 1)) {
                koga.SetDialogueID(4);
                // ___________Lance le dialogue de Koga
            }
            else if(!Inventory.instance.HasTool("Flower", 4)) {
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
            jouma.SetDialogueID(2);
            noona.SetDialogueID(6);
            halma.SetDialogueID(2);
            koga.SetScene("Village");
            noona.SetScene("Village");
            // ____________ mettre la position de Koga et Noona
        }
        if(Inventory.instance.HasTool("Recipient", 1) && !Inventory.instance.HasTool("Flower", 4)) {
            koga.SetDialogueID(5);
        }
        if(Inventory.instance.HasTool("Recipient",1) && Inventory.instance.HasTool("Flower", 4)) {
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
        if(Inventory.instance.HasTool("Nounours", 1)) {
            manai.SetDialogueID(2);
        }
        if(manai.HaveSeenDialogue(2)) {
            // _____________ Ajouter une clef à l'inventaire
            // _____________ Enlever le nounours de l'inventaire
            manai.SetDialogueID(3);
        }

        /////////////////////// BEAUTIFUL DRESS QUEST ///////////////////////
        if(Inventory.instance.HasTool("AleyaDress", 1)) {
            aleya.SetDialogueID(4);
        }
        if(aleya.HaveSeenDialogue(4)) {
            // ________________ Enlever la robe de l'inventaire
            // ________________ Ajouter la clef a l'inventaire 
            aleya.SetDialogueID(5);
        }

        /////////////////////// BEAUTIFUL DRESS QUEST ///////////////////////
        


        /////////////////////// END MAIN QUEST ///////////////////////
        if(aiki.HaveSeenDialogue(2) && inCrystalRoom == -1){
            aiki.SetDialogueID(3);
        }
        if(Inventory.instance.HasTool("Key", 1)) {
            aiki.SetDialogueID(4);
        }
        if(Inventory.instance.HasTool("Key", 10)) {
            aiki.SetDialogueID(5);
        }
        if(aiki.HaveSeenDialogue(5)) {
            aiki.SetDialogueID(6);
            pada.SetDialogueID(3);
            halma.SetDialogueID(3);
            byoldal.SetDialogueID(4);
            possa.SetDialogueID(4);
            migwa.SetDialogueID(5);
            jouma.SetDialogueID(3);
            // __________ Enlever les clefs de l'inventaire et les pierres ?
            // __________ Lancer l'animation de fin avec le discours de l'esprit de la forêt
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

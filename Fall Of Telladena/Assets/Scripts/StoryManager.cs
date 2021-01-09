//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public int inCrystalRoom = 0;
    Character[] characters;

    void Start() {
        characters = FindObjectsOfType<Character>();
    }
    void Update() {
        if(inCrystalRoom == 1) {
            //change dialogue de tous les persos ou presque
            // Move Aïki and change his dialogue
            foreach (Character pnj in characters) {
                if (pnj.name == "Aïki") {
                    pnj.SetScene("OutsideCastle");
                    pnj.SetDialogueID(1);
                    pnj.SetPosition(new Vector3(-20f, -10.1379f, -10f));
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject character;
    public Text myText;
    public int textId;
    public GameObject dialogueCanvas;

    void Update() {
        if (textId < character.GetComponent<Character>().dialogue.Length) {
            myText.text = character.GetComponent<Character>().dialogue[textId];
        }

        if (Input.GetKeyDown("p")) {
            if (character.GetComponent<Character>().isDialoguePossible) {
                dialogueCanvas.SetActive(true);
                Debug.Log("BOB");
            }
        }
    }
}

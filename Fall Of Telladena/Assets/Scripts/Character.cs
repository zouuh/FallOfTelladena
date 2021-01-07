//ZOE

using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // Public attributes
    public string[] dialogue;
    public GameObject dialogueCanvas;
    public Text dialogueNameText;
    public Text dialogueText;

    // Private attributes
    private static string myName;
    private int dialogueId = 0;
    private bool isDialoguePossible = false;

    // Getter and Setter
    public int GetDialogueId() {
        return dialogueId;
    }

    public void SetDialogueID(int newId) {
        dialogueId = newId;
    }

    void Start() {
        myName = this.name;
        dialogue = ReadCharacterFile();
    }

    void Update() {
        if (Input.GetKeyDown("p")) {        // Mettre la touche action correspondante
            // Test if the character is in the dialogue zone
            if (isDialoguePossible) {
                // Test if the dialogue window isn't active
                if (!dialogueCanvas.activeSelf) {
                    dialogueCanvas.SetActive(true);
                    dialogueNameText.text = this.name;
                    // Enable the right sentence of current Id
                    dialogueText.text = dialogue[dialogueId];
                }
                else {
                    dialogueCanvas.SetActive(false);
                }
            }
        }
    }

    // Change bool if this is in the dialogue zone
    void OnTriggerStay(Collider colliderInfo) {
        if (colliderInfo.CompareTag("DialogueInput")) {
            isDialoguePossible = true;
        }
    }

    // Change bool if this isn't in the dialogue zone
    void OnTriggerExit(Collider colliderInfo) {
        if (colliderInfo.CompareTag("DialogueInput")) {
             isDialoguePossible = false;
        }
    }

    static string[] ReadCharacterFile()
    {
        // Path of this character's document
        string path = "Assets/Documents/" + myName + ".txt";

        StreamReader reader = new StreamReader(path);

        Debug.Log(reader.ReadLine());

        // Get the number of sentences of the character
        int nbDialogue = int.Parse(reader.ReadLine());
        // Initiate the string array with the right size
        string[] dialogue = new string[nbDialogue];
        // Put sentences in this array
        for (int i=0; i<nbDialogue; i++) {
            string newLine = "";
            newLine += reader.ReadLine();
            dialogue[i] = newLine;
        }

        reader.Close();
        return dialogue;
    }


    // Save and load functions
    public void SaveCharacter() {
        SaveSystem.SaveCharacter(this, this.name);
    }
    
    public void LoadCharacterer() {
        CharacterData data = SaveSystem.LoadCharacter(this.name);

        dialogueId = data.dialogueId;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}

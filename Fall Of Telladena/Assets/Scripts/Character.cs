﻿//ZOE

using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    // Public attributes
    public string scene = "Village";
    public GameObject dialogueCanvas;
    public GameObject mainInterfaceCanvas;

    // Private attributes
    private static string myName;
    public int dialogueId = 0;
    private bool isDialoguePossible = false;
    private bool hasSeenDialogue = false;
    private string[] dialogue;
    private Text dialogueNameText;
    private Text dialogueText;

    // Constructor
    public Character(int newDialogueId, string newScene) {
        dialogueId = newDialogueId;
        scene = newScene;
    }
    
    // Getter and Setter
    public int GetDialogueId() {
        return dialogueId;
    }

    public void SetDialogueID(int newId) {
        dialogueId = newId;
        hasSeenDialogue = false;
    }

    public string GetScene() {
        return scene;
    }

    public void SetScene(string newScene) {
        scene = newScene;
    }

    public void SetPosition(Vector3 newPos) {
        // Change local position of player
        transform.position = newPos;
        //Save it for other scenes
        this.SaveCharacter();
    }
    
    public bool haveSeenDialogue(int id) {
        return (dialogueId == id && hasSeenDialogue);
    }

    void Start() {
        myName = this.name;
        dialogueNameText = dialogueCanvas.GetComponentsInChildren<Text>()[0];
        dialogueText = dialogueCanvas.GetComponentsInChildren<Text>()[1];
        dialogue = ReadCharacterFile();
        this.LoadCharacterer();
        Debug.Log("Test");
        if(SceneManager.GetActiveScene().name != scene) {
            Debug.Log("BOB");
            gameObject.SetActive(false);
        }
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
                    mainInterfaceCanvas.SetActive(false);
                }
                else {
                    dialogueCanvas.SetActive(false);
                    // Say thaa Oksusu red this dialogue
                    hasSeenDialogue = true;
                    mainInterfaceCanvas.SetActive(true);
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

    static string[] ReadCharacterFile() {
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
        Debug.Log("Save " + this.name);
        SaveSystem.SaveCharacter(this, this.name);
    }
    
    public void LoadCharacterer() {
        CharacterData data = SaveSystem.LoadCharacter(this.name);

        dialogueId = data.dialogueId;
        scene = data.scene;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}

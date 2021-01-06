using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string[] dialogue;
    public GameObject dialogueCanvas;
    public Text dialogueNameText;
    public Text dialogueText;
    private static string myName;

    public int dialogueId = 0;
    private bool isDialoguePossible = false;

    public int GetDialogueId() {
        return dialogueId;
    }
    void Start() {
        myName = this.name;
        dialogue = ReadString();
    }

    void Update() {
        if (Input.GetKeyDown("p")) {
            if (isDialoguePossible) {
                if (!dialogueCanvas.activeSelf) {
                    dialogueCanvas.SetActive(true);
                    dialogueNameText.text = this.name;
                    dialogueText.text = dialogue[dialogueId];
                }
                else {
                    dialogueCanvas.SetActive(false);
                }
            }
        }
    }

    public void ChangeDialogueID(int newId) {
        dialogueId = newId;
    }

    void OnTriggerStay(Collider colliderInfo) {
        if (colliderInfo.CompareTag("DialogueInput")) {
            isDialoguePossible = true;
        }
    }

    void OnTriggerExit(Collider colliderInfo) {
        if (colliderInfo.CompareTag("DialogueInput")) {
             isDialoguePossible = false;
        }
    }
    static string[] ReadString()
    {
        string path = "Assets/Documents/" + myName + ".txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        reader.ReadLine();
        int nbDialogue = int.Parse(reader.ReadLine());
        string[] dialogue = new string[nbDialogue];
        for (int i=0; i<nbDialogue; i++) {
            string newLine = "";
            newLine += reader.ReadLine();
            dialogue[i] = newLine;
        }
        reader.Close();
        return dialogue;
    }

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

using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string[] dialogue;
    private int dialogueId = 0;
    public bool isDialoguePossible = false;
    public GameObject dialogueCanvas;
    public Text dialogueNameText;
    public Text dialogueText;

    void Start() {
        dialogue = ReadString();
    }

    void Update() {
        if (Input.GetKeyDown("p")) {
            if (!dialogueCanvas.activeSelf) {
                if (isDialoguePossible) {
                    dialogueCanvas.SetActive(true);
                    dialogueNameText.text = this.name;
                    dialogueText.text = dialogue[dialogueId];
                }
            }
            else {
                dialogueCanvas.SetActive(false);
            }
        }
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
        string path = "Assets/Documents/Aïki.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadLine());
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
}

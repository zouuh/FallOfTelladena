/* 
 * Authors : Zoé 
 */

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    // Public attributes
    public GameObject dialogueCanvas;
    public GameObject mainInterfaceCanvas;

    // Private attributes
    private static string myName;
    private int dialogueId = 0;
    public string scene;
    private bool automaticDialogue = false;
    private bool isDialoguePossible = false;
    private bool hasSeenDialogue = false;
    private string[] dialogue;
    private Text dialogueNameText;
    private Text dialogueText;
    

    // Getter and Setter
    public int GetDialogueId() {
        return dialogueId;
    }

    public void SetDialogueID(int newId) {
        dialogueId = newId;
        hasSeenDialogue = false;
    }

    public string  GetScene() {
        return scene;
    }

    public void SetScene(string newScene) {
        scene = newScene;
        this.CheckScene();
    }

    public void SetAutomaticDialogue(bool newBool) {
        automaticDialogue = newBool;
    }

    public void SetPosition(Vector3 newPos) {
        // Change local position of player
        transform.position = newPos;
    }


    // Other methods
    public bool HaveSeenDialogue(int id) {
        return (dialogueId == id && hasSeenDialogue);
    }

    public void CheckScene() {
        if(SceneManager.GetActiveScene().name != scene) {
            foreach(Collider col in GetComponentsInChildren<Collider>()) {
                col.enabled = false;
            }
            foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
                rend.enabled = false;
            }
        }
        else {
            foreach(Collider col in GetComponentsInChildren<Collider>()) {
                col.enabled = true;
            }
            foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
                rend.enabled = true;
            }
        }
    }

    public void ActiveDialogue() {
        dialogueCanvas.SetActive(true);
        dialogueNameText.text = this.name;
        // Enable the right sentence of current Id
        dialogueText.text = "";
        // dialogueText.text = dialogue[dialogueId];
        mainInterfaceCanvas.SetActive(false);

        StartCoroutine(TypeSentence(dialogue[dialogueId]));
    }

    IEnumerator TypeSentence (string sentence) {
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void HideDialogue() {
        dialogueCanvas.SetActive(false);
        // Say that Oksusu red this dialogue
        hasSeenDialogue = true;
        mainInterfaceCanvas.SetActive(true);
    }

    static string[] ReadNpcFile() {
        // Path of this NPC's document
        string path = "Assets/Documents/Dialogue/" + myName + ".txt";

        StreamReader reader = new StreamReader(path);

        reader.ReadLine();

        // Get the number of sentences of the NPC
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

    // Unity functions
    void Start() {
        myName = this.name;
        dialogueCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("DialogueCanvas").gameObject;
        mainInterfaceCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("MainInterfaceCanvas").gameObject;
        dialogueNameText = dialogueCanvas.GetComponentsInChildren<Text>()[0];
        dialogueText = dialogueCanvas.GetComponentsInChildren<Text>()[1];
        dialogue = ReadNpcFile();
        this.LoadNPC();
        CheckScene();
        if(this.name == "Aïki") {
            automaticDialogue = true;
        }
    }

    void Update() {
        if (Input.GetKeyDown("p")) {        // Mettre la touche action correspondante
            // Test if the NPC is in the dialogue zone
            if (isDialoguePossible) {
                // Test if the dialogue window isn't active
                if (!dialogueCanvas.activeSelf) {
                    ActiveDialogue();
                }
                else {
                    HideDialogue();
                }
            }
        }
    }

    void OnTriggerEnter(Collider colliderInfo) {
        // Change bool if this is in the dialogue zone
        if (colliderInfo.CompareTag("DialogueInput")) {
            isDialoguePossible = true;
            if(automaticDialogue) {
                ActiveDialogue();
                automaticDialogue = false;
            }
        }
    }

    void OnTriggerExit(Collider colliderInfo) {
        // Change bool if this isn't in the dialogue zone
        if (colliderInfo.CompareTag("DialogueInput")) {
             isDialoguePossible = false;
        }
    }

    // Save and load functions
    public void SaveNPC() {
        SaveSystem.SaveNPC(this, this.name);
    }
    
    public void LoadNPC() {
        NPCData data = SaveSystem.LoadNPC(this.name);

        dialogueId = data.dialogueId;
        scene = data.scene;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    public void ResetNPC() {

        // Path of this NPC initial document
        string path = "Assets/Documents/Initial/" + this.name + "Initial.txt";

        StreamReader reader = new StreamReader(path);

        reader.ReadLine();

        // Set dialogue ID to 0
        this.dialogueId = 0;
        // Get the initial scene of the NPC
        this.scene = reader.ReadLine();
        // Get the initial position of the NPC
        float posx = float.Parse(reader.ReadLine());
        float posy = float.Parse(reader.ReadLine());
        float posz = float.Parse(reader.ReadLine());
        this.SetPosition(new Vector3(posx, posy, posz));
        // Get the initial rotation
        float rotx = float.Parse(reader.ReadLine());
        float roty = float.Parse(reader.ReadLine());
        float rotz = float.Parse(reader.ReadLine());
        this.transform.rotation = new Quaternion(rotx, roty, rotz, 0);

        reader.Close();

        this.CheckScene();
        this.SaveNPC();
    }
}

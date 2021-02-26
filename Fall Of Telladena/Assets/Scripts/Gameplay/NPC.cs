/* 
 * Authors : Zoé 
 */

using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    // Public attributes
    //public string initialScene;
    //public Vector3 initialPosition;
    public GameObject dialogueCanvas;
    public GameObject mainInterfaceCanvas;

    // Private attributes
    private static string myName;
    public int dialogueId = 0;
    public string scene;
    private bool isDialoguePossible = false;
    private bool hasSeenDialogue = false;
    private string[] dialogue;
    private Text dialogueNameText;
    private Text dialogueText;

    // Constructor
    public NPC(int newDialogueId, string newScene) {
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
        this.SaveNPC();
    }
    
    public bool HaveSeenDialogue(int id) {
        return (dialogueId == id && hasSeenDialogue);
    }

    public void checkScene() {
        if(SceneManager.GetActiveScene().name != scene) {
            // GetComponent<Rigidbody>().useGravity = false;
            foreach(Collider col in GetComponentsInChildren<Collider>()) {
                col.enabled = false;
            }
            foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
                rend.enabled = false;
            }
            //gameObject.SetActive(false);
        }
        else {
            // GetComponent<Rigidbody>().useGravity = true;
            foreach(Collider col in GetComponentsInChildren<Collider>()) {
                col.enabled = true;
            }
            foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
                rend.enabled = true;
            }
        }
    }

    void Start() {
        myName = this.name;
        dialogueCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("DialogueCanvas").gameObject;
        //FindObjectOfType<CanvasController>().dialogueCanvas;
        mainInterfaceCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("MainInterfaceCanvas").gameObject;
        dialogueNameText = dialogueCanvas.GetComponentsInChildren<Text>()[0];
        dialogueText = dialogueCanvas.GetComponentsInChildren<Text>()[1];
        dialogue = ReadNpcFile();
        this.LoadNPC();
        // if(SceneManager.GetActiveScene().name != scene) {
        //     gameObject.SetActive(false);
        // }
        checkScene();
        
    }

    void Update() {
        // Debug.Log("NPC " + this.name);
        if (Input.GetKeyDown("p")) {        // Mettre la touche action correspondante
            // Test if the NPC is in the dialogue zone
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

    // Save and load functions
    public void SaveNPC() {
        SaveSystem.SaveNPC(this, this.name);
        //Debug.Log("saved " + name);
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
        this.transform.rotation = new Quaternion(rotx, roty, rotz, 1);

        reader.Close();

        this.checkScene();
        this.SaveNPC();
    }
}

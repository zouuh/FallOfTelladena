/* 
 * Authors : Zoé, Manon
 */

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class NPC : MonoBehaviour
{
    [SerializeField]
    string actionName = "Parler";
    [SerializeField]
    ToolsManager toolManager;

    // PRIVATE ATTRIBUTES
    private static string myName;
    private int dialogueId = 0;
    private string scene;
    private bool automaticDialogue = false;
    private bool isDialoguePossible = false;
    private bool hasSeenDialogue = false;
    private string[] dialogue;
    private Text dialogueNameText;
    private Text dialogueText;
    private GameObject dialogueCanvas;
    private GameObject mainInterfaceCanvas;
    

    // GETTERS AND SETTERS
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

    // Used when NPC has automatic dialogues
    public void SetAutomaticDialogue(bool newBool) {
        automaticDialogue = newBool;
    }

    // Change local position of player
    public void SetPosition(Vector3 newPos) {
        transform.position = newPos;
    }


    // OTHER METHODS

    // Return true if the active dialogue have been red one time or more, else return false 
    public bool HaveSeenDialogue(int id) {
        return (dialogueId == id && hasSeenDialogue);
    }

    // Active NPC's colliders and meshes if he appears in the current scene, else unactive it
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

    // Take the right sentence and name and switch from main interface canvas to dialogue canvas
    public void ActiveDialogue() {
        dialogueCanvas.SetActive(true);
        dialogueNameText.text = this.name;
        dialogueText.text = dialogue[dialogueId];
        mainInterfaceCanvas.SetActive(false);

        // TO IMPLEMENT : text appearing progressively
        //StartCoroutine(TypeSentence(dialogue[dialogueId]));
    }

    // For "ActiveDialogue" -> display the text letter by letter (excepting css code)
    IEnumerator TypeSentence (string sentence) {
        for(int i=0; i<sentence.Length; ++i) {
            char letter = sentence[i];
            if(letter.Equals("<")) {
                Debug.Log("couleur");
                string word = "<";
                while (!letter.Equals(">")) {
                    ++i;
                    letter = sentence[i];
                    word += letter;
                }
                ++i;
                letter = sentence[i];
                word += letter;
                while (!letter.Equals(">")) {
                    ++i;
                    letter = sentence[i];
                    word += letter;
                }
                dialogueText.text += word;
                yield return null;
            }
            dialogueText.text += letter;
            yield return null;
        }
    }

    // Switch from dialogue canvas to main interface canvas and update dialogue as "seen"
    public void HideDialogue() {
        dialogueCanvas.SetActive(false);
        hasSeenDialogue = true;
        mainInterfaceCanvas.SetActive(true);
    }

    // Read the .txt files with dialogue of NPCs and return a list of sentences
    static string[] ReadNpcFile() {
        return Resources.Load<TextAsset>("Documents/Dialogue/" + myName).text.Split('\n').Skip(2).ToArray();

        // OLD CODE : check if we need to keep some elements        
        // // Path of this NPC's document
        // string path = "Assets/StreamingAssets/" + myName + ".txt";

        // StreamReader reader = new StreamReader(path);

        // reader.ReadLine();

        // // Get the number of sentences of the NPC
        // int nbDialogue = int.Parse(reader.ReadLine());
        // // Initiate the string array with the right size
        // string[] dialogue = new string[nbDialogue];
        // // Put sentences in this array
        // for (int i=0; i<nbDialogue; i++) {
        //     string newLine = "";
        //     newLine += reader.ReadLine();
        //     dialogue[i] = newLine;
        // }
        // reader.Close();
        // return dialogue;
    }

    // UNITY FUNCTIONS

    void Start() {
        myName = this.name;
        dialogueCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("DialogueCanvas").gameObject;
        mainInterfaceCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("MainInterfaceCanvas").gameObject;
        toolManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ToolsManager>();
        dialogueNameText = dialogueCanvas.GetComponentsInChildren<Text>()[0];
        dialogueText = dialogueCanvas.GetComponentsInChildren<Text>()[1];
        dialogue = ReadNpcFile();
        this.LoadNPC();     // To update every NPC with the story advencement
        CheckScene();
        
        // TO DO : Fix automatic dialogues system
        // if(this.name == "Aïki") {
        //     automaticDialogue = true;
        // }
    }

    void Update() {
        if (isDialoguePossible) {       // If Oksusu is in the dialogue zone of a character
            if (Input.GetButtonUp("Action") && !toolManager.usingATool) {     // If dialogue dialoge request is demanded
                
                Vector3 playerPos = toolManager.gameObject.transform.root.position;
                Vector3 NPCPos = transform.position;
                toolManager.transform.LookAt(new Vector3(NPCPos.x, playerPos.y, NPCPos.z));

                //transform.LookAt(new Vector3(playerPos.x, NPCPos.y, playerPos.z));
                //transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
                Vector3 relativePos = new Vector3(playerPos.x, NPCPos.y, playerPos.z) - transform.position;
                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
                transform.rotation = rotation * Quaternion.Euler(0, 90, 0);

                toolManager.StartCoroutine("UseTool");

                // Test if the dialogue window is active to display or hide it
                if (!dialogueCanvas.activeSelf)
                {
                    ActiveDialogue();
                }
                else
                {
                    HideDialogue();
                }
            }
        }
    }

    // change dialogue possibility to true if Oksusu is in the dialogue zone and run automatic dialogues
    void OnTriggerEnter(Collider colliderInfo) {
        if (colliderInfo.CompareTag("DialogueInput")) {
            isDialoguePossible = true;
            if(automaticDialogue) {
                ActiveDialogue();
                automaticDialogue = false;
            }
            else {
                toolManager.canDrop = false;
                toolManager.ActivateActionInfo(actionName, 1, null);
            }
        }
    }

    // Change dialogue possibility to false if Oksusu exits the dialogue zone
    void OnTriggerExit(Collider colliderInfo) {
        if (colliderInfo.CompareTag("DialogueInput")) {
            isDialoguePossible = false;
            toolManager.canDrop = true;
            toolManager.DeactivateActionInfo();
        }
    }

    // SAVE AND LOAD FUNCTIONS
    public void SaveNPC() {
        SaveSystem.SaveNPC(this, this.name);
    }
    
    // Take NPC's infos from saving file and save it in attributes
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

    // Take NPC's initial infos from .txt file and save it in attributes
    public void ResetNPC() {
        // Path of this NPC initial document
        string path = "Assets/StreamingAssets/Documents/Initial/" + this.name + "Initial.txt";

        StreamReader reader = new StreamReader(path);

        if(reader != null) {
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
        }

        // Display or not the NPC after reseted it and save the new values
        this.CheckScene();
        this.SaveNPC();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    [SerializeField]
    GameObject mainViewCanvas;
    [SerializeField]
    GameObject mapCanvas;
    [SerializeField]
    GameObject inventoryCanvas;
    [SerializeField]
    GameObject pauseCanvas;
    [SerializeField]
    GameObject dialogueCanvas;
    [SerializeField]
    GameObject loadingCanvas;
    [SerializeField]
    GameObject optionsCanvas;
    [SerializeField]
    ActionCanvas actionCanvas;


    // needed for the map
    [SerializeField]
    GameObject player; // the player

    [SerializeField]
    MapPlayerPosition playerPositionSprite; // the marker for player

    void Start() {
        loadingCanvas.SetActive(false);
    }

    void Update() {
        if(Input.GetButtonDown("Inventory")) {
            if(inventoryCanvas.activeSelf) {
                SwitchCanvas(inventoryCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, inventoryCanvas);
            }
        }
        if(Input.GetButtonDown("Map")) {
            if(mapCanvas.activeSelf) {
                SwitchCanvas(mapCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf)
            {
                playerPositionSprite.UpdatePlayerPosition();
                SwitchCanvas(mainViewCanvas, mapCanvas);
            }
        }
        if (Input.GetButtonDown("Cancel")) {
            if(mainViewCanvas.activeSelf) {
                mainViewCanvas.SetActive(false);
                pauseCanvas.SetActive(true);
            }
            if(optionsCanvas.activeSelf) {
                optionsCanvas.SetActive(false);
                pauseCanvas.SetActive(true);
            }
            else {
                mapCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                pauseCanvas.SetActive(false);
                dialogueCanvas.SetActive(false);
                mainViewCanvas.SetActive(true);
            }
        }
    }

    public void SwitchCanvas(GameObject oldCanvas, GameObject newCanvas) {
        oldCanvas.SetActive(false);
        newCanvas.SetActive(true);
    }

    public void TurnOnActionCanvas(string actionName="Use", int amount = 1, string tool = "", bool active = true)
    {
        actionCanvas.UpdateText(actionName, amount, tool, active);
        actionCanvas.gameObject.SetActive(true);
    }
    
    public void TurnOffActionCanvas()
    {
        actionCanvas.gameObject.SetActive(false);
    }
}

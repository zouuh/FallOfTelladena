using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    GameObject mainViewCanvas;
    GameObject mapCanvas;
    GameObject inventoryCanvas;
    GameObject pauseCanvas;
    GameObject dialogueCanvas;
    GameObject loadingCanvas;

    void Start() {
        Canvas[] allCanvas = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canvas in allCanvas) {
            switch (canvas.name) {
                case "MainInterfaceCanvas":
                    mainViewCanvas = canvas.gameObject;
                    break;
                case "MapCanvas":
                    mapCanvas = canvas.gameObject;
                    break;
                case "InventoryCanvas":
                    inventoryCanvas = canvas.gameObject;
                    break;
                case "DialogueCanvas":
                    dialogueCanvas = canvas.gameObject;
                    break;
                case "PauseCanvas":
                    pauseCanvas = canvas.gameObject;
                    break;
                case "LoadingScreen":
                    loadingCanvas = canvas.gameObject;
                    break;
                default :
                    // Debug.Log(canvas.name + " not linked ");
                    break;
            }
        }
    }

    void Update() {
        if(Input.GetKeyDown("l")) {
            if(inventoryCanvas.activeSelf) {
                SwitchCanvas(inventoryCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, inventoryCanvas);
            }
        }
        if(Input.GetKeyDown("m")) {
            if(mapCanvas.activeSelf) {
                SwitchCanvas(mapCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, mapCanvas);
            }
        }
        if(Input.GetKeyDown("escape")) {
            if(pauseCanvas.activeSelf) {
                SwitchCanvas(pauseCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, pauseCanvas);
            }
        }
    }

    public void SwitchCanvas(GameObject oldCanvas, GameObject newCanvas) {
        oldCanvas.SetActive(false);
        newCanvas.SetActive(true);
    }
}

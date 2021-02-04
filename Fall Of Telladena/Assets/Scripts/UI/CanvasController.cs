using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    public GameObject mainViewCanvas;
    GameObject mapCanvas;
    GameObject inventoryCanvas;
    GameObject pauseCanvas;
    public GameObject dialogueCanvas;
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
        loadingCanvas.SetActive(false);
    }

    void Update() {
        Debug.Log("CanvasController");
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
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, mapCanvas);
            }
        }
        if (Input.GetButtonDown("Cancel")) {
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
        Debug.Log("New : " + newCanvas.name + " -> " + newCanvas.activeSelf);
        Debug.Log("Old : " + oldCanvas.name + " -> " + oldCanvas.activeSelf);
    }
}

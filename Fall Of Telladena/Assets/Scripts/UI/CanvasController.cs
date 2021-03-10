/*
 * Authors : Zoé, Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
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
    ActionCanvas actionCanvas;

    void Start() {
        loadingCanvas.SetActive(false);
    }

    void Update() {
        //Debug.Log("CanvasController");
        if(Input.GetButtonDown("Inventory")) {
            if(inventoryCanvas.activeSelf) {
                SwitchCanvas(inventoryCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, inventoryCanvas);
            }
        }
        if(Input.GetButtonDown("Map"))
        {
            Debug.Log("map 1 !");
            if (mapCanvas.activeSelf) {
                SwitchCanvas(mapCanvas, mainViewCanvas);
            }
            else if(mainViewCanvas.activeSelf) {
                SwitchCanvas(mainViewCanvas, mapCanvas);
                Debug.Log("map 2 !");
            }
        }
        if (Input.GetButtonDown("Cancel")) {
            if(mainViewCanvas.activeSelf) {
                mainViewCanvas.SetActive(false);
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
        Debug.Log("New : " + newCanvas.name + " -> " + newCanvas.activeSelf);
        Debug.Log("Old : " + oldCanvas.name + " -> " + oldCanvas.activeSelf);
    }

    public void TurnOnActionCanvas(string actionName="Use", string tool = "", bool active = true)
    {
        actionCanvas.UpdateText(actionName, tool, active);
        actionCanvas.gameObject.SetActive(true);
    }
    public void TurnOffActionCanvas()
    {
        actionCanvas.gameObject.SetActive(false);
    }
}

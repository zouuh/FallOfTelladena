/*
 * Authors : Zoé, Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuCanvas;
    [SerializeField]
    GameObject optionsCanvas;
    [SerializeField]
    GameObject loadGameCanvas;
    [SerializeField]
    GameObject creditsCanvas;
    [SerializeField]
    GameObject loadingCanvas;

    void Start() {
        loadingCanvas.SetActive(false);
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if(!mainMenuCanvas.activeSelf) {
                optionsCanvas.SetActive(false);
                loadGameCanvas.SetActive(false);
                creditsCanvas.SetActive(false);
                mainMenuCanvas.SetActive(true);
            }
        }
    }

    public void SwitchCanvas(GameObject oldCanvas, GameObject newCanvas) {
        oldCanvas.SetActive(false);
        newCanvas.SetActive(true);
    }
}

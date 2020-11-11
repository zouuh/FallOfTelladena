using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveToolBar : MonoBehaviour
{
    private Transform activeToolTransform;
    private GameObject[] toolButtons;
    private int activeToolID = 0;

    public Sprite image1;
    public Sprite image2;
    void Start() {
        activeToolTransform = transform.GetChild(0);
    }

    void Update() {
        if(Input.GetKeyDown("e")) {
            activeToolID++;
            if (activeToolID > 5) {
                activeToolID = 5;
            }
            ChangeActiveTool(activeToolID);
        }
        if(Input.GetKeyDown("a")) {
            activeToolID--;
            if (activeToolID < 0) {
                activeToolID = 0;
            }
            ChangeActiveTool(activeToolID);
        }
    }

    public void ChangeActiveTool(int activeID) {
        activeToolTransform.gameObject.GetComponent<Image>().sprite = image2; 
        activeToolTransform = transform.GetChild(activeID);
        activeToolTransform.gameObject.GetComponent<Image>().sprite = image1;
        activeToolID = activeID;
    }
}

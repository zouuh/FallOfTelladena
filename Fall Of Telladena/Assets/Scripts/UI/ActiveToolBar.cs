//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveToolBar : MonoBehaviour
{
    private int activeToolTransform;
    [SerializeField]
    private Image[] toolButtons;
    private int activeToolID = 0;

    public Sprite activeSprite;
    public Sprite unactiveSprite;

    void Start() {
        // default active tool is the first on left
        // activeToolTransform = transform.GetChild(0);
    }

    public void ChangeActiveTool(int activeID) {
        // Change sprite to unactive
        toolButtons[activeToolID].sprite = unactiveSprite;
        // Change sprite of the new active to active 
        activeToolID = activeID;
        toolButtons[activeToolID].sprite = activeSprite;
    }
}

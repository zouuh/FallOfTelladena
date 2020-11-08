using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject canvas;

    public void DisableCanvas() {
        canvas.SetActive(false);
    }

    public void ActivateCanvas() {
        canvas.SetActive(true);
    }
}

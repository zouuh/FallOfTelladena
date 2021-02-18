using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BimbopCave : MonoBehaviour
{

    public void Appear()
    {
        GetComponent<Animation>().Play("BimbopCave");
        // shake camera
        // fix player and play fix camera
    }
}

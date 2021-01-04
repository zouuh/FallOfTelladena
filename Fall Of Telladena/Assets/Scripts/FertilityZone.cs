using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilityZone : MonoBehaviour
{
    //public Collider selectedBridge = null;
    public string selectedBridge = null;
    public bool aBridgeIsSelected = false;
    int hasLeave = -1;

    // Update is called once per frame
    void Update()
    {
        if (hasLeave == 0)
        {
            selectedBridge = null;
            aBridgeIsSelected = false;
            hasLeave = -1;
        }
        else if (hasLeave > 0)
        {
            --hasLeave;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bridge1") || other.CompareTag("Bridge2") || other.CompareTag("Bridge3"))
        {
            //UnityEngine.Debug.Log("selected");
            hasLeave = 10;
            selectedBridge = other.tag;
            aBridgeIsSelected = true;
        }
        else
        {
            //UnityEngine.Debug.Log(other.tag);
        }
    }
}

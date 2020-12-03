using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject myLedge;
    //public GameObject myLedgeExit;
    GameObject[] myChildObjects;

    void OnTriggerEnter(Collider other)
    {
        /*myChildObjects.Push(other.gameObject);
        for (int i = 0; i < myChildObjects.Length; ++i)
        {
            myChildObjects[i].transform.parent = myLedge.transform;
        }*/
        other.gameObject.transform.parent = myLedge.transform;
    }

    void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
    }
}

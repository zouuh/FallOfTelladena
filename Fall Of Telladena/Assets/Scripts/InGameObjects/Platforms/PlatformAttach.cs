using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject myLedge;
    GameObject player = null;
    //public GameObject myLedgeExit;
    //GameObject[] myChildObjects;

    void OnTriggerEnter(Collider other)
    {
        /*myChildObjects.Push(other.gameObject);
        for (int i = 0; i < myChildObjects.Length; ++i)
        {
            myChildObjects[i].transform.parent = myLedge.transform;
        }*/
        if (other.CompareTag("ContactZone"))
        {
            if(player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }
            player.transform.parent = myLedge.transform;
        }
        else if(!other.CompareTag("FertilityZone"))
        {
            other.gameObject.transform.parent = myLedge.transform;
        }        
        //other.gameObject.transform.SetParent(myLedge.transform, true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            if (player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }
            player.gameObject.transform.parent = null;
        }
        else if (!other.CompareTag("FertilityZone"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}

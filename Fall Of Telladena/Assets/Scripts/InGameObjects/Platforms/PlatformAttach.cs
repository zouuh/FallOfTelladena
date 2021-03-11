using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject myLedge;
    GameObject player = null;
    //Transform playerParent = null;
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
            //playerParent = player.transform.parent;

            Vector3 tmpScale = new Vector3(player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
            player.transform.parent = myLedge.transform;
            //player.transform.localScale = Vector3.one;
            player.transform.localScale = tmpScale;
        }
        else if(!other.CompareTag("FertilityZone") && !other.CompareTag("ContactZoneBrambles") && !other.CompareTag("FacingWaterZone") && !other.CompareTag("ContactZoneNests"))
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
            DontDestroyOnLoad(player.gameObject); // important : avoid bug when deparenting from DontDestroyOnLoad Object
        }
        else if (!other.CompareTag("FertilityZone") && !other.CompareTag("ContactZoneBrambles") && !other.CompareTag("FacingWaterZone") && !other.CompareTag("ContactZoneNests"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}

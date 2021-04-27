/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BridgeParticleController : MonoBehaviour
{
    public GameObject dest;
    int hasLeave = 0;
    Collider selectedBridge = null;
    // Start is called before the first frame update
    void Start()
    {
        dest.SetActive(false);
        //UnityEngine.Debug.Log("hi");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //this.transform.position = dest.transform.position;
            //this.transform.parent = dest.transform;
            dest.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            //this.transform.parent = null;
            dest.SetActive(false);
        }

        if(hasLeave < 0)
        {
            selectedBridge = null;
            hasLeave = 0;
        }
        else if(hasLeave > 0)
        {
            --hasLeave;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            hasLeave = 10;
            selectedBridge = other;
        }
    }
}

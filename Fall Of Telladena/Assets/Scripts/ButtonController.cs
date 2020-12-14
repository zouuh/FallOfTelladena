using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    int frameBeforeCheckingAgain = 10;
    public bool isOn = false;
    public Vector3 positionUp = new Vector3(0, 1, 0);
    public Vector3 positionDown = new Vector3(0, 0.3f, 0);
    public GameObject ButtonToPush;

    public DoorController[] doorsToControl;
    int nbOfColliders = 0;

    public float switchActivationWeight = 0.01f;
    
    /*
    // Update is called once per frame
    void LateUpdate()
    {
        /*
        if (isOn && this.transform.position.y != positionDown.y)
        {
            //this.transform.position = positionDown;
            ButtonToPush.SetActive(false);
        }
        *
        if (frameBeforeCheckingAgain <= 0)
        {
            if(isOn)
            {
                isOn = false;
                //this.transform.position = positionUp;
                //ButtonToPush.SetActive(true);
                //UnityEngine.Debug.Log("isOff");
            }
            frameBeforeCheckingAgain = 10;
        }
        --frameBeforeCheckingAgain;
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log(other.tag);
        if (!other.CompareTag("ButtonBase") && ((other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > switchActivationWeight) || other.CompareTag("ContactZone")))
        {
            /*
            isOn = true;
            //ButtonToPush.SetActive(false);
            if (other.GetComponent<Rigidbody>() != null) // stop bouncing
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
            }    
            */
            UnityEngine.Debug.Log(other.tag + " : isOn");
            ++nbOfColliders;

            foreach(DoorController door in doorsToControl)
            {
                door.open();
            }
        }
        /*
        else
        {
            isOn = false;
            UnityEngine.Debug.Log(other.tag + " : isOff");
        }
        
        void OnTriggerExit()
        {
            isOn = false;
            UnityEngine.Debug.Log(other.tag + " : isOff");
        }
        */

    }

    void OnTriggerExit(Collider other)
    {
        --nbOfColliders;
        if (nbOfColliders <= 0)
        {
            UnityEngine.Debug.Log(other.tag + " : isOff");

            foreach (DoorController door in doorsToControl)
            {
                door.close();
            }
        }

        /*
        if (!other.CompareTag("ButtonBase") && ((other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > switchActivationWeight) || other.CompareTag("ButtonZone")))
        {
            UnityEngine.Debug.Log(other.tag + " : isOff");

            foreach (DoorController door in doorsToControl)
            {
                door.close();
            }
        }
        */
    }
}

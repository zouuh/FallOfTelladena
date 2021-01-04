using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenityDoorController : MonoBehaviour
{
    public GameObject myDoor;
    bool isOpen = false;
    int timeBeforeCheckingAgain = 10;


    void Start()
    {
        myDoor.SetActive(true);
    }

    private void Update()
    {
        
        if(timeBeforeCheckingAgain <= 0){
            if (isOpen && myDoor.activeSelf)
            {
                myDoor.SetActive(false);
                //UnityEngine.Debug.Log("open");
            }
            else if (!isOpen)
            {
                myDoor.SetActive(true);
                //UnityEngine.Debug.Log("close");
            }
            isOpen = false;
            timeBeforeCheckingAgain = 10;
        }
        else
        {
            --timeBeforeCheckingAgain;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SerenityInput"))
        {
            //myDoor.SetActive(false);
            isOpen = true;
            //timeBeforeCheckingAgain = 10;
        }
        /*
        void OnTriggerExit()
        {
            myDoor.SetActive(true);
            UnityEngine.Debug.Log("close");
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoorController : MonoBehaviour
{
    public GameObject[] listOfSwitches;
    public GameObject myDoor;
    bool isOpen = false;
    int frameBeforeCheckingAgain = 10;

    // Update is called once per frame
    void Update()
    {
        if(frameBeforeCheckingAgain <= 0)
        {
            isOpen = true;
            frameBeforeCheckingAgain = 10;
            for (var i = 0; i < listOfSwitches.Length; i++)
            {
                if (!listOfSwitches[i].GetComponent<ButtonController>().isOn)
                {
                    isOpen = false;
                }
            }
            /*
            if (isOpen)
            {
                myDoor.SetActive(false);
            }
            else
            {
                myDoor.SetActive(true);
            }*/
            myDoor.SetActive(!isOpen);
        }
        --frameBeforeCheckingAgain;
        
    }
}

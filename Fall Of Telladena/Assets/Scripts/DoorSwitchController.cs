using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchController : MonoBehaviour
{
    public SwitchLightController[] listOfBtns;
    public GameObject myDoor;
    bool isActivated = true;
    int frameBeforeCheckingAgain = 30;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (frameBeforeCheckingAgain <= 0)
        {
            for (int i = 0; i < listOfBtns.Length; ++i)
            {
                if (!listOfBtns[i].isOn)
                {
                    isActivated = false;
                }
            }

            if (isActivated)
            {
                myDoor.SetActive(false);
            }
            else
            {
                myDoor.SetActive(true);
            }

            isActivated = true;
            frameBeforeCheckingAgain = 30;
        }
        --frameBeforeCheckingAgain;
        
    }
}

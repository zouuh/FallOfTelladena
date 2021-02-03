using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject myDoor;
    public void open()
    {
        Debug.Log("open");
        myDoor.SetActive(false);
    }

    public void close()
    {
        Debug.Log("close");
        myDoor.SetActive(true);
    }
}

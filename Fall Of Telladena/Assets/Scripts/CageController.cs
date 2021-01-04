using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public GameObject door;

    void OpenDoor()
    {
        door.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRoomDoor : MonoBehaviour
{
    void OnTriggerEnter() {
        if(FindObjectOfType<StoryManager>().inCrystalRoom < 2) {
            FindObjectOfType<StoryManager>().inCrystalRoom ++;
        }
    }
}

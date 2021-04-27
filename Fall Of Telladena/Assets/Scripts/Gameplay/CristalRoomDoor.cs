/*
 * Authors : Zoé
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRoomDoor : MonoBehaviour
{
    // Update story manager's parameter to begin the quests
    void OnTriggerEnter() {
        if(FindObjectOfType<StoryManager>().inCrystalRoom < 2) {
            FindObjectOfType<StoryManager>().inCrystalRoom ++;
        }
    }
}

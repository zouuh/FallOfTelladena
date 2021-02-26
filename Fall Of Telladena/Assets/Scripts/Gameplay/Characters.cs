/* 
 * Authors : Zoé 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public void ResetAllNPC() {
        foreach(NPC npc in GetComponentsInChildren<NPC>()) {
            npc.ResetNPC();
        }
    }
}

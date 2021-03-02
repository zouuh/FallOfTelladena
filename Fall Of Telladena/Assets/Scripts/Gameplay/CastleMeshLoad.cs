using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleMeshLoad : MonoBehaviour
{
    [SerializeField]
    GameObject[] rooms;

    void OnTriggerExit(Collider col) {
        if(col.name == "Oksusu") {
            foreach(GameObject room in rooms) {
                room.SetActive(!room.activeSelf);
            }
        }
    }
}

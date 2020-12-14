using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCageController : MonoBehaviour
{
    public string itemName;
    public Transform respawnZone;
    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item") && other.gameObject.GetComponent<Item>().itemName == itemName) // the right item is inside
        {            
            // open the door
            door.SetActive(false);
            Debug.Log("open door");

            // play sound
        }
        else
        {
            // reject object (play animation)
            Debug.Log("Hodor!");
            other.gameObject.transform.position = respawnZone.position;

            // play sound
        }
    }
}

/*
 * Authors : Manon
 */

using UnityEngine;

public class InputCageController : MonoBehaviour
{
    public string itemName;
    public Transform respawnZone;
    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item") && other.gameObject.GetComponent<ItemPickup>().item.name == itemName) // the right item is inside
        {            
            // open the door
            door.SetActive(false);

            // play sound
        }
        else
        {
            // reject object (play animation)
            other.gameObject.transform.position = respawnZone.position;

            // play sound
        }
    }
}

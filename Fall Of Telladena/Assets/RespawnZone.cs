using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in water !");
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = other.gameObject.GetComponent<PlayerPositionManager>().lastSafePosition;
            other.gameObject.SetActive(true);
        }
    }
}

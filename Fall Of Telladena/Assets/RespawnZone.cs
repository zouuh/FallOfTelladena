using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField]
    bool fixedSpawnPos = false;
    [SerializeField]
    Transform spawnPos = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in water !");
            other.gameObject.SetActive(false);
            if (fixedSpawnPos)
            {
                other.gameObject.transform.position = spawnPos.position;
            }
            else
            {
                other.gameObject.transform.position = other.gameObject.GetComponent<PlayerPositionManager>().lastSafePosition;
            }
            other.GetComponentInChildren<FacingWaterZone>().floattingText.desactivate();
            other.gameObject.SetActive(true);
        }
    }
}

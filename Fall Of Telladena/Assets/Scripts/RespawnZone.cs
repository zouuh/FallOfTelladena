using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField]
    bool fixedSpawnPos = false;
    [SerializeField]
    Transform spawnPos = null;

    GameObject[] platformsToReset;

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
                other.gameObject.transform.rotation = spawnPos.rotation;

                // reset platforms
                if(platformsToReset == null)
                {
                    platformsToReset = GameObject.FindGameObjectsWithTag("WaterPlatform");
                }
                foreach (GameObject platform in platformsToReset)
                {
                    platform.GetComponent<WaterPlatformController>().ResetPosition();
                }
            }
            else
            {
                other.gameObject.transform.position = other.gameObject.GetComponent<PlayerPositionManager>().lastSafePosition;
            }
            other.GetComponent<ToolsManager>().DeactivateActionInfo();
            other.gameObject.SetActive(true);
        }
    }
}

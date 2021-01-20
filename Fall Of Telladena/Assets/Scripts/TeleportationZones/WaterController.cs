/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public Transform resetPos;
    GameObject[] platformsToReset;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("contact");
        if (other.CompareTag("Player"))
        {
            // begin transition
            // ...

            // begin teleportation
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = resetPos.transform.position;
            other.gameObject.transform.rotation = resetPos.transform.rotation;

            // reset platforms
            // ...
            platformsToReset = GameObject.FindGameObjectsWithTag("WaterPlatform");
            foreach (GameObject platform in platformsToReset)
            {
                platform.GetComponent<WaterPlatformController>().resetPosition();
            }

            // end transition
            // ...

            // end teleportation
            other.gameObject.SetActive(true);

        }
    }
}
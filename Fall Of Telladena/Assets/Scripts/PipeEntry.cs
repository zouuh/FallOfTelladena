using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEntry : MonoBehaviour
{
    bool hasAnObject = false;
    int timer = 300;
    GameObject spawnee;
    public Transform exitPos;

    public AudioManager audioManager;

    private void Update()
    {
        if (hasAnObject)
        {
            if (timer > 0)
            {
                --timer;
            }
            else
            {
                timer = 300;
                spawnee.transform.position = exitPos.position;

                // stop sound
                audioManager.Stop("pipeSound");

                spawnee.SetActive(true);
                spawnee.GetComponent<Item>().inPipe = false;
                spawnee = null;
                hasAnObject = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && !hasAnObject && other.gameObject.GetComponent<Item>().authorizedInPipe && !other.gameObject.GetComponent<Item>().inPipe)
        {
            // set inactive
            other.gameObject.SetActive(false);

            other.gameObject.GetComponent<Item>().inPipe = true;
            spawnee = other.gameObject;
            hasAnObject = true;

            // play a sound
            audioManager.Play("pipeSound");

            // wait for timer
            // set to exitPos
            // set active
        }
    }
}

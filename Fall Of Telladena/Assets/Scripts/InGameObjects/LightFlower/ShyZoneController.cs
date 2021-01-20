using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyZoneController : MonoBehaviour
{
    bool playerIsTooClose = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsTooClose)
        {
            //Get the Renderer component from the new cube
            var myRenderer = transform.parent.GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
        }
        else
        {
            //Get the Renderer component from the new cube
            var myRenderer = transform.parent.GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
        }

        playerIsTooClose = false;
    }
    
    void onTriggerStay(Collider other)
    {
        UnityEngine.Debug.Log("Collision.");
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Flower is closed.");
            playerIsTooClose = true;
        }
    }
    /*
    
    void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;
        // Now do whatever you need with myCollider.
        // (If multiple colliders were involved in the collision, 
        // you can find them all by iterating through the contacts)
        if (myCollider.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Flower is closed.");
            playerIsTooClose = true;
        }
    }
    */
}

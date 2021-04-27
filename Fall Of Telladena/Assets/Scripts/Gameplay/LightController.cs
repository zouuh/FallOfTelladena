/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.tag == "Torch")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
            //myLight = GetComponentInParent<Light>();
            //myLight.intensity = 1;
        }
    }*/
    /*
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Torch")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            UnityEngine.Debug.Log("Do something here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Ground")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            UnityEngine.Debug.Log("Do something else here");
        }
        
        
    }
    */
}

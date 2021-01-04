using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    //Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.tag == "LightInput")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
            //myLight = GetComponentInParent<Light>();
            //myLight.intensity = 1;
        }
    }
}

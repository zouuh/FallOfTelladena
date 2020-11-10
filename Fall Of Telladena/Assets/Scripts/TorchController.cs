using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TorchController : MonoBehaviour

{
    Light myLight;
    public GameObject myLightZone;
    public float timeBeforeNewLightInput;
    float timerBeforeNewLightInput;
    bool hasReceivedLight = false;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        //myLightZone = GameObject.Find("LightPower");
        myLightZone.SetActive(false);
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
    }

    // Update is called once per frame
    void Update()
    {
        //myLight.intensity = 1; // Mathf.PingPong(Time.time, 8);
        if (timerBeforeNewLightInput > 0)
        {
            --timerBeforeNewLightInput;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightInput") && myLight.intensity <= 0 && !hasReceivedLight && timerBeforeNewLightInput <= 0)
        {
            myLight.intensity = 1;
            myLightZone.SetActive(true);

            timerBeforeNewLightInput = 60 * timeBeforeNewLightInput;

            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white*2);

            hasReceivedLight = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
            if (!hasReceivedLight && timerBeforeNewLightInput <= 0 && myLight.intensity <= 0)
            {
                myLight.intensity = 1;
                myLightZone.SetActive(true);

                timerBeforeNewLightInput = 60 * timeBeforeNewLightInput;

                //Get the Renderer component from the new cube
                var myRenderer = GetComponent<Renderer>();
                //Call SetColor using the shader property name "_Color" and setting the color to red
                myRenderer.material.SetColor("_EmissionColor", Color.white * 2);

                hasReceivedLight = true;
            }else if(myLight.intensity > 0 && timerBeforeNewLightInput <= 0 && other.CompareTag("LightInputPlayer"))
            {
                myLight.intensity = 0;
                myLightZone.SetActive(false);

                timerBeforeNewLightInput = 60 * timeBeforeNewLightInput;

                //Get the Renderer component from the new cube
                var myRenderer = GetComponent<Renderer>();
                //Call SetColor using the shader property name "_Color" and setting the color to red
                myRenderer.material.SetColor("_EmissionColor", Color.white * 0);

                hasReceivedLight = false;
            }
    }
}

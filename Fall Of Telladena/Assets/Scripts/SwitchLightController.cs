using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwitchLightController : MonoBehaviour
{
    public bool isOn = false;
    int frameBeforeCheckingAgain = 30;
    // Start is called before the first frame update
    void Start()
    {
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
    }
    
    void LateUpdate()
    {
        if (frameBeforeCheckingAgain <= 0)
        {
            if (isOn)
            {
                var myRenderer = GetComponent<Renderer>();
                //Call SetColor using the shader property name "_Color" and setting the color to red
                myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
                //UnityEngine.Debug.Log("ison");
                //isOn = true;
            }
            else
            {
                var myRenderer = GetComponent<Renderer>();
                //Call SetColor using the shader property name "_Color" and setting the color to red
                myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
                //UnityEngine.Debug.Log("isoff");
            }
            isOn = false;
            frameBeforeCheckingAgain = 30;
        }        
        --frameBeforeCheckingAgain;
    }
    

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            /*
            UnityEngine.Debug.Log("is colliding");
            
            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
            */
            isOn = true;
            UnityEngine.Debug.Log("ison");
        }/*else
        {
            UnityEngine.Debug.Log("is not colliding");
            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
        }*/
    }
    /*
    void OnTriggerExit(Collider other)
    {
        UnityEngine.Debug.Log("is not colliding");
        UnityEngine.Debug.Log(other.tag);
        if (other.gameObject.CompareTag("LightInput"))
        {
            UnityEngine.Debug.Log("is away");
            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
        }
    }*/
}

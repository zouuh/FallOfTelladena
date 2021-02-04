using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoLightPlantController : MonoBehaviour
{
    public bool isOut = true;
    public bool playerIsOut = true;
    public GameObject[] myParts;
    //public CharacterController myPlayer;
    MovementInput myPlayerMovement;
    int frameBeforeCheckingAgain = 10;
    //public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerMovement = GameObject.Find("Jammo_Player").GetComponent<MovementInput>();
        /*
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
        */
    }
    
    void LateUpdate()
    {
        if (frameBeforeCheckingAgain <= 0)
        {
            for (var i = 0; i < myParts.Length; ++i)
            {
                myParts[i].SetActive(isOut);
            }

            if (!isOut)
            {
                isOut = true;
            }
            if (!playerIsOut)
            {
                playerIsOut = true;
                myPlayerMovement.Velocity = 10;
            }
            frameBeforeCheckingAgain = 10;
        }

        --frameBeforeCheckingAgain;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            isOut = false;
            //UnityEngine.Debug.Log("isOff");
        }
        if (other.CompareTag("Player"))
        {
            playerIsOut = false;
            myPlayerMovement.Velocity = 2;
            UnityEngine.Debug.Log("touch");
            //UnityEngine.Debug.Log("isOff");
        }
    }
    /*
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            isOut = true;
            //UnityEngine.Debug.Log("isOn");
        }
    }
    */
    
}

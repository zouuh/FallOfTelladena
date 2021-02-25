using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrambleController : MonoBehaviour
{
    public bool isOut = true;
    public bool playerIsOut = true;
    public GameObject[] myParts;
    //public CharacterController myPlayer;
    PlayerMovement myPlayerMovement;
    float normalVelocity;
    [SerializeField]
    float velocityInBrambles = 2.0f;
    //public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        normalVelocity = myPlayerMovement.speedWithBrambles;
        /*
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
        */
    }
    /*
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
    */

    /*
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
    */
    void Appear()
    {
        for (var i = 0; i < myParts.Length; ++i)
        {
            myParts[i].SetActive(true);
        }
    }

    void Disappear()
    {
        for (var i = 0; i < myParts.Length; ++i)
        {
            myParts[i].SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            Disappear();
        }

        if (other.CompareTag("ContactZoneBrambles"))
        {
            myPlayerMovement.speedWithBrambles = velocityInBrambles;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            Appear();
        }

        if (other.CompareTag("ContactZoneBrambles"))
        {
            myPlayerMovement.speedWithBrambles = normalVelocity;
        }
    }

}

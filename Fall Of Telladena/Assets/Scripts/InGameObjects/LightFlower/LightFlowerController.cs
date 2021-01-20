using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LightFlowerController : MonoBehaviour
{
    public bool isOn = true;
    public Transform dest;
    bool isPickedUp = false;
    public GameObject myLightZone;
    int frameBeforeCheckingAgain = 10;
    MovementInput playerMovementInput;
    int timerBeforeNotAfraidAnymore = 30;

    void PickUp()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = dest.position;
        this.transform.parent = GameObject.Find("Destination").transform;
    }

    void PutDown()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
        myLightZone.SetActive(true);

        //Get current player velocity
        playerMovementInput = GameObject.Find("Jammo_Player").GetComponent<MovementInput>();
    }

    void UpdatePickUp()
    {
        if (isPickedUp)
        {
            PickUp();
            UnityEngine.Debug.Log("pick");
        }
        else
        {
            PutDown();
            UnityEngine.Debug.Log("Down");
        }
        
    }
    /*
    void Update()
    {
        if (isOn)
        {
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
        }
        isOn = true;
    }*/

    void SetOn()
    {
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
        myLightZone.SetActive(true);
    }

    void SetOff()
    {
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
        myLightZone.SetActive(false);
    }
    void LateUpdate()
    {
        if (frameBeforeCheckingAgain <= 0)
        {
            if (isOn)
            {
                if (timerBeforeNotAfraidAnymore <= 0)
                {
                    SetOn();
                }
                else
                {
                    //timerBeforeNotAfraidAnymore--;
                }
            }
            else
            {
                SetOff();
                timerBeforeNotAfraidAnymore = 30;
            }
            if (!Input.GetKeyUp(KeyCode.E))
            {
                isOn = true;
            }

            frameBeforeCheckingAgain = 10;
        }
        --frameBeforeCheckingAgain;
        if (timerBeforeNotAfraidAnymore > 0)
        {
            --timerBeforeNotAfraidAnymore;
        }        

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FlowerContactZone"))
        {
            if(playerMovementInput.Velocity >= 10) //if player is running
            {
                isOn = false;
            }
            else if(timerBeforeNotAfraidAnymore <=0) // if player is not running (and the flower is not afraid anymore), he can grab the flower
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp();
                    isPickedUp = true;
                    //UpdatePickUp();
                }
            }

            // in any case
            if (isPickedUp && Input.GetKeyUp(KeyCode.E))
            {
                PutDown();
                isPickedUp = false;
            }

            /*
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
            myLightZone.SetActive(false);
            */


        }
    }
    /*
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlowerContactZone"))
        {
            isOn = true;
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
            myLightZone.SetActive(true);
        }
    }
    */
}

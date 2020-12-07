using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlatformController : MonoBehaviour
{
    int position = 0;
    int accumulator = 1;
    public GameObject myPlatform;
    //bool isPressingKey = false;
    //public Vector3 scaleChange;
    //string animName = "";

    // Animations
    private Animator anim;

    void changeAnimation()
    {
        position += accumulator;
        UnityEngine.Debug.Log(position);

        anim = myPlatform.GetComponent<Animator>();
        UnityEngine.Debug.Log("waterPlatform_0" + (accumulator < 0 ? position + 1 : position) + (accumulator > 0 ? "" : "_backwards"));
        anim.Play("waterPlatform_0" + (accumulator < 0 ? position + 1 : position) + (accumulator > 0 ? "" : "_backwards"));

        if (position > 1 || position < 1)
        {
            accumulator = -accumulator;
        }
    }
    /*
    void LateUpdate()
    {
        if (frameBeforeCheckingAgain <= 0)
        {
            if (isOn)
            {
                var myRenderer = GetComponent<Renderer>();
                //Call SetColor using the shader property name "_Color" and setting the color to red
                myRenderer.material.SetColor("_EmissionColor", Color.white * 1);
                myLightZone.SetActive(true);
            }
            else
            {
                var myRenderer = GetComponent<Renderer>();
                //Call SetColor using the shader property name "_Color" and setting the color to red
                myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
                myLightZone.SetActive(false);
            }
            if (!Input.GetKeyUp(KeyCode.E))
            {
                isOn = true;
            }
            frameBeforeCheckingAgain = 30;
        }
        --frameBeforeCheckingAgain;

    }
    */

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            changeAnimation();
            Destroy(other.gameObject);
        }
    }
}

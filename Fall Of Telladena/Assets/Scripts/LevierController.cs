using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierController : MonoBehaviour
{
    int position = 0;
    int accumulator = 1;
    public GameObject myLevier;
    public GameObject[] listOfPlatforms;
    bool isPressingKey = false;
    public Vector3 scaleChange;
    //string animName = "";

    // Animations
    private Animator anim;


    void changePosition()
    {
        position+=accumulator;
        UnityEngine.Debug.Log(position);
        //myLevier.transform.Rotate(0, 0, (20 - position * 20), Space.Self);
        myLevier.transform.rotation = Quaternion.Euler(0, 0, (20 - position * 20));
        for(int i=0; i<listOfPlatforms.Length; ++i)
        {
            listOfPlatforms[i].transform.localScale += scaleChange*accumulator;
            if(listOfPlatforms[i].transform.localScale.y > 2*scaleChange.y)
            {
                listOfPlatforms[i].transform.localScale -= 3*scaleChange;
            }
        }
        if (position > 1 || position < 1)
        {
            accumulator = -accumulator;
        }
    }
    void changeAnimation()
    {
        position += accumulator;
        UnityEngine.Debug.Log(position);
        //myLevier.transform.Rotate(0, 0, (20 - position * 20), Space.Self);
        myLevier.transform.rotation = Quaternion.Euler(0, 0, (20 - position * 20));
        for (int i = 0; i < listOfPlatforms.Length; ++i)
        {
            anim = listOfPlatforms[i].GetComponent<Animator>();
            UnityEngine.Debug.Log("platform_up0" + (accumulator<0?position+1:position) + (accumulator > 0 ? "" : "_backwards"));
            //animName = "platform_anim0" + (accumulator < 0 ? position + 1 : position) + (accumulator > 0 ? "" : "_backwards");
            anim.Play("platform_up0" + (accumulator < 0 ? position + 1 : position) + (accumulator > 0 ? "" : "_backwards"));
        }
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

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            if (Input.GetKeyDown(KeyCode.I) && !isPressingKey /*&& listOfPlatforms[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1*/)
            {
                isPressingKey = true;
                //changePosition();
                changeAnimation();
            }else if(isPressingKey && Input.GetKeyUp(KeyCode.I))
            {
                isPressingKey = false;
            }
        }
    }
}

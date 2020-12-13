using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlatformController : MonoBehaviour
{
    int position = 0;
    int accumulator = 1;
    public GameObject myPlatform;
    public Vector3 initPosition;
    public int nbOfSteps;
    public int currStep = 0;
    public bool forward = true;

    // Animations
    private Animator anim;

    private void Start()
    {
        initPosition = myPlatform.transform.position;
        anim = myPlatform.GetComponent<Animator>();
    }

    /*
     * void changeAnimation()
    {
        position += accumulator;
        UnityEngine.Debug.Log(position);

        //anim = myPlatform.GetComponent<Animator>();
        UnityEngine.Debug.Log("waterPlatform_0" + (accumulator < 0 ? position + 1 : position) + (accumulator > 0 ? "" : "_backwards"));
        anim.Play("waterPlatform_0" + (accumulator < 0 ? position + 1 : position) + (accumulator > 0 ? "" : "_backwards"));

        if (position > 1 || position < 1)
        {
            accumulator = -accumulator;
        }
    }
    */
    void changeAnimation()
    {
        if (currStep >= nbOfSteps)
        {
            currStep = 0;
            forward = !forward;
        }
        if (currStep < nbOfSteps)
        {
            Debug.Log("Play:" + currStep);
            anim.Play("waterPlatform_" + (forward ? "01" : "backward"));
        }

        ++currStep;
    }

    public void resetPosition()
    {
        Debug.Log("reset");
        anim.Play("resetWaterPlatform");
        myPlatform.transform.position = initPosition;
        position = 0;
        accumulator = 1;
        currStep = 0;
        forward = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            changeAnimation();
            Destroy(other.gameObject);
        }
    }
}

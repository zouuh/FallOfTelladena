using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierController : MonoBehaviour
{
    public GameObject myLevier;
    public GameObject[] listOfPlatforms;

    public int nbOfSteps;
    public string axisToAnimate; // x, y, z
    int axisId;
    int currStep = 0;
    public int forwardOrBackward = 1; // 1 = forward, -1 = backward

    // Animations
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;

    private void Start()
    {
        switch (axisToAnimate)
        {
            case "x":
                axisId = 0;
                break;
            case "y":
                axisId = 1;
                break;
            case "z":
                axisId = 2;
                break;
            default:
                axisId = 0;
                break;
        }

    }
    /*
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
    */

    void changeAnimation()
    {
        Debug.Log("anim");
        if (currStep >= nbOfSteps)
        {
            currStep = 0;
            forwardOrBackward *= -1;
        }
        if (currStep < nbOfSteps)
        {
            Debug.Log("Play:" + currStep);
            for(int i = 0; i < listOfPlatforms.Length; ++i)
            {
                myAnimation = listOfPlatforms[i].GetComponent<Animation>();
                // Create custom animation
                AnimationClip clip = new AnimationClip();
                //clip = myAnimation.clip;
                clip.legacy = true;

                Keyframe[] keys;
                keys = new Keyframe[2];
                keys[0] = new Keyframe(0.0f, listOfPlatforms[i].transform.position[axisId]);
                keys[1] = new Keyframe(1.0f, listOfPlatforms[i].transform.position[axisId] + (1.0f * forwardOrBackward));
                curve = new AnimationCurve(keys);
                clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);
                Debug.Log("localPosition." + axisToAnimate);

                myAnimation.AddClip(clip, clip.name);
                // Play custom animation
                myAnimation.Play(clip.name);
            }

            
        }

        ++currStep;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ContactZone") && Input.GetKeyUp(KeyCode.I))
        {
            changeAnimation();
                
            /*
            if (Input.GetKeyUp(KeyCode.I) /*&& listOfPlatforms[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1*)
            {
                Debug.Log("anim");
                changeAnimation();
            }*/
        }
    }
}

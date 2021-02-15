using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierController : MonoBehaviour
{
    GameObject player = null;

    public GameObject myLever;
    public Platform[] listOfPlatforms;
    bool isInContact = false;

    /*
    public int nbOfSteps;
    public string axisToAnimate; // x, y, z
    int axisId;
    int currStep = 0;
    [SerializeField]
    float stepSize = 1.0f;
    public int forwardOrBackward = 1; 
    */

    // Animations
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;

    /*
    private int GetAxisToAnimate(string axis)
    {
        switch (axis)
        {
            case "x":
                return 0;
            case "y":
                return 1;
            case "z":
                return 2;
            default:
                return 0;
        }

    }
    */

    void Update()
    {
        if(isInContact && Input.GetButtonDown("Action"))
        {
            changeAnimation();
            player.transform.LookAt(transform.position);
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

        //Debug.Log("Play:" + currStep);
        for(int i = 0; i < listOfPlatforms.Length; ++i)
        {
            Debug.Log(listOfPlatforms[i]);
            Debug.Log(listOfPlatforms[i].currStep);
            Debug.Log(listOfPlatforms[i].nbOfSteps);
            // if last step is reached, go backwards
            if (listOfPlatforms[i].currStep >= listOfPlatforms[i].nbOfSteps)
            {
                listOfPlatforms[i].currStep = 0;
                listOfPlatforms[i].forwardOrBackward *= -1;
            }

            myAnimation = listOfPlatforms[i].GetComponent<Animation>();
            // Create custom animation
            AnimationClip clip = new AnimationClip();
            //clip = myAnimation.clip;
            clip.legacy = true;

            int axisId = listOfPlatforms[i].axisId;

            float key1 = listOfPlatforms[i].transform.localPosition[axisId];
            Debug.Log(listOfPlatforms[i].transform.localPosition[axisId]);
            float key2 = key1 + (listOfPlatforms[i].stepSize * listOfPlatforms[i].forwardOrBackward);
            clip.name = (key1 + "-" + key2);

            Keyframe[] keys;
            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, key1);
            keys[1] = new Keyframe(1.0f, key2);
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition." + listOfPlatforms[i].axisToAnimate, curve);
            //Debug.Log("localPosition." + axisToAnimate);

            myAnimation.AddClip(clip, clip.name);
            // Play custom animation
            myAnimation.Play(clip.name);

            ++listOfPlatforms[i].currStep;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZone") /*&& Input.GetKeyUp(KeyCode.I)*/)
        {
            isInContact = true;
            Debug.Log("isInCOntact");
            if(player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }
            //changeAnimation();
                
            /*
            if (Input.GetKeyUp(KeyCode.I) /*&& listOfPlatforms[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1*)
            {
                Debug.Log("anim");
                changeAnimation();
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            Debug.Log("not in contact");
            isInContact = false;
        }
    }
}

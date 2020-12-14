using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlatformController : MonoBehaviour
{
    public GameObject myPlatform;
    public int nbOfSteps;
    public string axisToAnimate; // x, y, z
    int axisId;
    int currStep = 0;
    public int forwardOrBackward = 1; // 1 = forward, -1 = backward

    // Animations
    Animator anim;
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;

    private void Start()
    {
        myAnimation = myPlatform.GetComponent<Animation>();
        anim = myPlatform.GetComponent<Animator>();
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
    void changeAnimation()
    {
        if (currStep >= nbOfSteps)
        {
            currStep = 0;
            forwardOrBackward *= -1;
        }
        if (currStep < nbOfSteps)
        {
            Debug.Log("Play:" + currStep);

            // Create custom animation
            AnimationClip clip = new AnimationClip();
            //clip = myAnimation.clip;
            clip.legacy = true;

            Keyframe[] keys;
            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, myPlatform.transform.position[axisId]);
            keys[1] = new Keyframe(1.0f, myPlatform.transform.position[axisId] + (1.0f * forwardOrBackward));
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);
            Debug.Log("localPosition." + axisToAnimate);

            myAnimation.AddClip(clip, clip.name);
            // Play custom animation
            myAnimation.Play(clip.name);
        }

        ++currStep;
    }

    public void resetPosition()
    {
        Debug.Log("reset");

        // Create reset animation (one frame)
        AnimationClip clip = new AnimationClip();
        //clip = myAnimation.clip;
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[1];
        keys[0] = new Keyframe(0.0f, 0.0f);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);

        // Play custom animation
        myAnimation.AddClip(clip, clip.name);
        myAnimation.Play(clip.name);

        currStep = 0;
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

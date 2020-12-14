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
    public CharacterController player;

    // Animations
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;

    private void Start()
    {
        myAnimation = myPlatform.GetComponent<Animation>();
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

        // Create custom animation
        //AnimationClip clip = new AnimationClip();
        clip = myAnimation.clip;
        //clip.legacy = true;
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
            Keyframe[] keys;
            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, myPlatform.transform.position[axisId]);
            keys[1] = new Keyframe(1.0f, myPlatform.transform.position[axisId] + (1.0f*forwardOrBackward));
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition."+axisToAnimate, curve);
            Debug.Log("localPosition." + axisToAnimate);

            //animation.AddClip(clip, clip.name);
            player.enabled = false;
            // Play custom animation
            myAnimation.Play(clip.name);

            //animation.Play("waterPlatform_" + (forward ? "01" : "backward"));
        }

        ++currStep;
    }

    public void resetPosition()
    {
        Debug.Log("reset");

        // Create reset animation (one frame)
        Keyframe[] keys;
        keys = new Keyframe[1];
        keys[0] = new Keyframe(0.0f, 0.0f);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);

        // Play custom animation
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

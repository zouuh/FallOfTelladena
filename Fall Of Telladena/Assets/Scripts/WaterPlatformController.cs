using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlatformController : MonoBehaviour
{
    public GameObject myPlatform;
    public int nbOfSteps;
    int currStep = 0;
    int forwardOrBackward = 1; // 1 = forward, -1 = backward

    // Animations
    private Animator anim;
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;

    private void Start()
    {
        anim = myPlatform.GetComponent<Animator>();
        myAnimation = myPlatform.GetComponent<Animation>();

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
            

            // create a curve to move the GameObject and assign to the clip
            Keyframe[] keys;
            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, myPlatform.transform.position.z);
            keys[1] = new Keyframe(1.0f, myPlatform.transform.position.z + (1.0f*forwardOrBackward));
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

            //animation.AddClip(clip, clip.name);

            // Play custom animation
            myAnimation.Play(clip.name);

            //animation.Play("waterPlatform_" + (forward ? "01" : "backward"));
        }

        ++currStep;
    }

    public void resetPosition()
    {
        Debug.Log("reset");
        anim.Play("resetWaterPlatform");
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

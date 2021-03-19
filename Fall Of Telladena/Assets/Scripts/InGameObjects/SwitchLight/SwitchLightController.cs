/*
 * Authors : Manon
 */

using UnityEngine;
using System.Collections.Generic;

public class SwitchLightController : MonoBehaviour
{
    public bool isOn = false;
    [SerializeField]
    List<Platform> listOfPlatforms = new List<Platform>();

    // Animations
    Animation myAnimation;
    AnimationCurve curve;

    void Start()
    {
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            isOn = true;
            var myRenderer = GetComponent<Renderer>();
            myRenderer.material.SetColor("_EmissionColor", Color.white * 1);

            if (GetComponent<CinematicTrigger>() != null)
            {
                GetComponent<CinematicTrigger>().Play();
                ChangeAnimation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightInput") || other.CompareTag("LightInputPlayer"))
        {
            isOn = false;
            var myRenderer = GetComponent<Renderer>();
            myRenderer.material.SetColor("_EmissionColor", Color.white * 0);

            if (GetComponent<CinematicTrigger>() != null)
            {
                GetComponent<CinematicTrigger>().Play();
                ChangeAnimation();
            }
        }
    }

    void ChangeAnimation()
    {
        for (int i = 0; i < listOfPlatforms.Count; ++i)
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
            keys[1] = new Keyframe(listOfPlatforms[i].animationDuration, key2);
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition." + listOfPlatforms[i].axisToAnimate, curve);

            // new event created
            AnimationEvent evt;
            evt = new AnimationEvent();
            evt.time = listOfPlatforms[i].animationDuration; // same as key2 duration
            evt.functionName = "EndAnimation";

            clip.AddEvent(evt);

            myAnimation.AddClip(clip, clip.name);
            // Play custom animation
            listOfPlatforms[i].animationEnd = false;
            myAnimation.Play(clip.name);

            ++listOfPlatforms[i].currStep;
        }
    }
}

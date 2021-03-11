using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestController : MonoBehaviour
{
    public Symbol mySymbol;
    public Transform eggPosition;
    bool nestIsEmpty = true;
    public List<Platform> listOfPlatforms = new List<Platform>();

    // Animations
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Egg") && nestIsEmpty)
        {
            // Position Egg in center
            other.gameObject.transform.parent = this.transform;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.transform.position = eggPosition.position;
            other.gameObject.transform.rotation = this.transform.rotation;

            // Change Symbol aspect according to egg color
            mySymbol.myEgg = other.GetComponent<Item>().name;
            mySymbol.changeColor(other.gameObject.GetComponent<ItemMaze>().itemName);
            if (mySymbol.CheckWin())
            {
                GetComponent<CinematicTrigger>().Play();
                ChangeAnimation();
            }

            nestIsEmpty = false;
        }
    }

    private void OnTriggerExit(Collider other) // turn off symbol when removing the egg
    {
        if (other.CompareTag("Egg"))
        {
            // Position Egg in center
            other.gameObject.transform.parent = null;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Change Symbol aspect according to egg color
            mySymbol.myEgg = null;
            mySymbol.setDefaultColor();

            nestIsEmpty = true;

            // reverse animation
            if (mySymbol.CheckWin())
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

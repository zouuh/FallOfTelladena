/*
 * Authors : Manon
 */

using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    GameObject player = null;

    public GameObject myLever;
    public List<Platform> listOfPlatforms = new List<Platform>();
    bool isInContact = false;
    [SerializeField]
    bool triggerCinematic = false;

    //[SerializeField]
    //FloattingText floattingText;

    [SerializeField]
    string requiredToolName = "";
    [SerializeField]
    string actionName = "Use";

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

    bool HasRequiredTool()
    {
        if(requiredToolName.Length <= 0)
        {
            return true;
        }else if (Inventory.instance.isUsingTool(requiredToolName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (isInContact)
        {
            if (HasRequiredTool())
            {
                if (Input.GetButtonDown("Action") && listOfPlatforms.Find(x => x.animationEnd == false) == null && !player.GetComponent<ToolsManager>().usingATool)
                {
                    if(triggerCinematic)
                    {
                        GetComponent<CinematicTrigger>().Play();
                    }
                    ChangeAnimation();
                    player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
                    player.GetComponent<ToolsManager>().StartCoroutine("UseTool");
                }
                //floattingText.activate();
            }
            else
            {
                //floattingText.activate(requiredToolName);
            }
            player.GetComponent<ToolsManager>().ActivateActionInfo(actionName, requiredToolName);
        }
    }

    void ChangeAnimation()
    {
        for(int i = 0; i < listOfPlatforms.Count; ++i)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            isInContact = true;
            if(player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }
            player.GetComponent<ToolsManager>().canDrop = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            isInContact = false;
            player.GetComponent<ToolsManager>().canDrop = true;
            //floattingText.desactivate();
            player.GetComponent<ToolsManager>().DeactivateActionInfo();
        }
    }
}

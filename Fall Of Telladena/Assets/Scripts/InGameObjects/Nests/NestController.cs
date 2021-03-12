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

    ToolsManager toolsManager = null;
    bool isInContact = false;

    GameObject egg;

    [SerializeField]
    List<string> requiredUsingTools = new List<string>() { "Blue Egg", "Green Egg", "Red Egg", "Silver Egg", "Golden Egg", "Purple Egg" };

    bool win = false;

    private void Update()
    {
        if(isInContact && Input.GetButtonDown("Action") && !toolsManager.usingATool)
        {
            toolsManager.UseTool();

            // Animation
            toolsManager.GetComponent<Animator>().SetBool("pickUp", true);

            if (nestIsEmpty)
            {
                // Put Egg in center
                egg = Instantiate(Inventory.instance.usedItem.prefab, eggPosition.position, eggPosition.rotation, eggPosition);
                egg.GetComponent<Rigidbody>().isKinematic = false;
                egg.GetComponent<ItemPickup>().canPickUp = false;

                Inventory.instance.Remove(Inventory.instance.usedItem);
                if(Inventory.instance.usedItem == null)
                {
                    toolsManager.CarryItem(false);
                }

                // Change Symbol aspect according to egg color
                mySymbol.myEgg = egg.GetComponent<ItemPickup>().item.name;
                mySymbol.changeColor(egg.GetComponent<ItemPickup>().item.name);
                if (mySymbol.CheckWin())
                {
                    GetComponent<CinematicTrigger>().Play();
                    ChangeAnimation();
                    win = true;
                }
                nestIsEmpty = false;
                toolsManager.ActivateActionInfo("Take", null, "Egg");
            }
            else
            {
                egg.GetComponent<ItemPickup>().canPickUp = true;
                Destroy(eggPosition.GetChild(0).gameObject);

                Inventory.instance.Add(egg.GetComponent<ItemPickup>().item);

                // Change Symbol aspect according to egg color
                mySymbol.myEgg = null;
                mySymbol.setDefaultColor();

                // reverse animation only if the door was open before
                if (win)
                {
                    GetComponent<CinematicTrigger>().Play();
                    ChangeAnimation();
                    win = false;
                }

                nestIsEmpty = true;
                toolsManager.ActivateActionInfo("Drop", requiredUsingTools, "Egg");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZoneNests"))
        {
            if(toolsManager == null)
            {
                toolsManager = other.GetComponent<ContactZone>().player.GetComponent<ToolsManager>();
            }

            isInContact = true;
            toolsManager.canDrop = false;

            if (nestIsEmpty)
            {
                toolsManager.ActivateActionInfo("Drop", requiredUsingTools, "Egg");
            }
            else
            {
                toolsManager.ActivateActionInfo("Take", null, "Egg");
            }
        }
    }

    private void OnTriggerExit(Collider other) // turn off symbol when removing the egg
    {

        if (other.CompareTag("ContactZoneNests"))
        {

            isInContact = false;
            toolsManager.canDrop = true;
            toolsManager.DeactivateActionInfo();
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

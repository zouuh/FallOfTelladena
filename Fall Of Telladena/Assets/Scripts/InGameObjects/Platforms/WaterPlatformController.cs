/*
 * Authors : Manon
 */

using UnityEngine;

public class WaterPlatformController : MonoBehaviour
{
    public GameObject myPlatform;
    public int nbOfSteps;
    public string axisToAnimate; // x, y, z
    int axisId;
    int currStep = 0;
    public int forwardOrBackward = 1; // 1 = forward, -1 = backward
    int initialForwardOrBackward = 1;
    public CharacterController myPlayer; // public because used by WaterPlatform
    ToolsManager toolsManager;

    // Animations
    Animator anim;
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;
    public bool animationIsEnded = true; // public because used by WaterPlatform

    bool isInContact = false;
    GameObject player = null;

    //[SerializeField]
    //FloattingText floattingText;

    [SerializeField]
    string requiredToolName; // Filled recipient
    [SerializeField]
    string actionName = "Drop water";
    [SerializeField]
    Item emptyRecipient; // Empty recipient

    private void Start()
    {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        toolsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ToolsManager>();
        myAnimation = myPlatform.GetComponent<Animation>();
        anim = myPlatform.GetComponent<Animator>();

        initialForwardOrBackward = forwardOrBackward;

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

    private void Update()
    {
        if (isInContact)
        {
            toolsManager.ActivateActionInfo(actionName, 1, requiredToolName, null);
            if (Inventory.instance.isUsingTool(requiredToolName))
            {
                if (Input.GetButtonUp("Action") && !toolsManager.usingATool)
                {
                    toolsManager.StartCoroutine("UseTool");
                    // get water
                    Debug.Log("Drop water.");

                    Inventory.instance.RemoveByName(requiredToolName);
                    Inventory.instance.Add(emptyRecipient);
                    Inventory.instance.ChangeActiveTool(emptyRecipient);

                    ChangeAnimation();
                    player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));

                    toolsManager.CarryItem(true, emptyRecipient);
                    toolsManager.DeactivateActionInfo();
                }
            }
        }
    }

    void ChangeAnimation()
    {
        // Block player
        myPlayer.enabled = false;

        if (currStep >= nbOfSteps)
        {
            currStep = 0;
            forwardOrBackward *= -1;
        }
        if (currStep < nbOfSteps)
        {
            // Create custom animation
            AnimationClip clip = new AnimationClip();
            clip.name = "Anim";
            //clip = myAnimation.clip;
            clip.legacy = true;

            Keyframe[] keys;
            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, myPlatform.transform.position[axisId]);
            keys[1] = new Keyframe(1.0f, myPlatform.transform.position[axisId] + (1.0f * forwardOrBackward));
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);
            Debug.Log("localPosition." + axisToAnimate);

            // Add event when animation end
            // new event created
            AnimationEvent evt = new AnimationEvent();
            //AnimationEvent[] listOfEvts = new AnimationEvent[1];
            //listOfEvts[0] = evt;

            evt.time = clip.length; // because animation lasts 1s
            evt.functionName = "animationEnd";
            clip.AddEvent(evt);

            //clip.events = listOfEvts;
            //AnimationUtility.SetAnimationEvents(clip, listOfEvts);

            myAnimation.AddClip(clip, clip.name);
            //Debug.Log(myAnimation["Anim"].clip.events.Length);
            animationIsEnded = false;
            // Play custom animation
            myAnimation.Play(clip.name);
            //yield return new WaitForSeconds(1.0f);
            //animationIsEnded = false;
            //wait();
            //animationEnd();
        }

        ++currStep;
    }

    public void ResetPosition()
    {
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
        forwardOrBackward = initialForwardOrBackward;
}

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ContactZone"))
        {
            if (player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }
            isInContact = true;
            toolsManager.canDrop = false;
            other.GetComponent<ContactZone>().player.GetComponentInChildren<FacingWaterZone>().isFacingWater = false;

        }

        /*
        if (other.CompareTag("Water") && animationIsEnded)
        {
            changeAnimation();
            Destroy(other.gameObject);
            
        }
        */
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("ContactZone"))
        {
            isInContact = false;
            toolsManager.canDrop = true;
            other.GetComponent<ContactZone>().player.GetComponentInChildren<FacingWaterZone>().isFacingWater = true;
            //floattingText.desactivate();
            toolsManager.DeactivateActionInfo();

        }
    }

}

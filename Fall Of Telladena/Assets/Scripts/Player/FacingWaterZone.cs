/*
 * Authors : Manon
 */

using UnityEngine;

public class FacingWaterZone : MonoBehaviour
{
    [SerializeField]
    string requiredToolName; // Empty recipient
    [SerializeField]
    string actionName = "Fill recipient";
    [SerializeField]
    Item filledRecipient; // Filled recipient
    [SerializeField]
    AudioManager audioManager;

    //[SerializeField]
    //public FloattingText floattingText; // public because used in RespawnZone
    [SerializeField]
    ToolsManager toolManager;

    public bool isFacingWater = false;

    private void Update()
    {
        if (isFacingWater)
        {
            toolManager.canDrop = false;
            toolManager.ActivateActionInfo(actionName, 1, requiredToolName);
            if (Inventory.instance.isUsingTool(requiredToolName))
            {
                if (Input.GetButtonUp("Action") && !toolManager.usingATool)
                {
                    GetComponentInParent<PlayerMovement>().animator.SetBool("pickUp", true);
                    audioManager.Play("fill_water");

                    toolManager.StartCoroutine("UseTool");
                    // get water
                    Debug.Log("Get water.");

                    toolManager.CarryItem(true, filledRecipient);

                    Inventory.instance.RemoveByName(requiredToolName);
                    Inventory.instance.Add(filledRecipient);
                    Inventory.instance.ChangeActiveTool(filledRecipient);

                    toolManager.DeactivateActionInfo();
                }
                //floattingText.activate();
            }
            else
            {
                //floattingText.activate(requiredToolName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            toolManager.canDrop = false;
            isFacingWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            toolManager.canDrop = true;
            isFacingWater = false;
            //floattingText.desactivate();
            toolManager.DeactivateActionInfo();
        }
    }

}

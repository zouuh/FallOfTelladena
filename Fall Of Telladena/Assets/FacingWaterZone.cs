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

    //[SerializeField]
    //public FloattingText floattingText; // public because used in RespawnZone
    [SerializeField]
    ToolsManager toolManager;

    public bool isFacingWater = false;

    private void Update()
    {
        if (isFacingWater)
        {
            toolManager.ActivateActionInfo(actionName, requiredToolName);
            if (Inventory.instance.isUsingTool(requiredToolName))
            {
                if (Input.GetButtonUp("Action") && !toolManager.usingATool)
                {
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
            isFacingWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isFacingWater = false;
            //floattingText.desactivate();
            toolManager.DeactivateActionInfo();
        }
    }

}

/*
 * Authors : Manon
 */

using UnityEngine;

public class FacingWaterZone : MonoBehaviour
{
    [SerializeField]
    string requiredToolName; // Empty recipient
    [SerializeField]
    string actionName = "Remplir le Vase";
    [SerializeField]
    Item filledRecipient; // Filled recipient

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
                    toolManager.StartCoroutine("UseTool");

                    toolManager.CarryItem(true, filledRecipient);

                    Inventory.instance.RemoveByName(requiredToolName);
                    Inventory.instance.Add(filledRecipient);
                    Inventory.instance.ChangeActiveTool(filledRecipient);

                    toolManager.DeactivateActionInfo();
                }
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

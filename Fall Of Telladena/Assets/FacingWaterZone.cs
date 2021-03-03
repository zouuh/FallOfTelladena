/*
 * Authors : Manon
 */
using System.Collections.Generic;
using UnityEngine;

public class FacingWaterZone : MonoBehaviour
{
    [SerializeField]
    string requiredToolName; // Empty recipient
    [SerializeField]
    Item filledRecipient; // Filled recipient

    [SerializeField]
    public FloattingText floattingText; // public because used in RespawnZone

    bool isFacingWater = false;

    private void Update()
    {
        if (isFacingWater)
        {
            if (Inventory.instance.isUsingTool(requiredToolName))
            {
                if (Input.GetButtonDown("Action"))
                {
                    // get water
                    Debug.Log("Get water.");

                    Inventory.instance.RemoveByName(requiredToolName);
                    Inventory.instance.Add(filledRecipient);
                    Inventory.instance.ChangeActiveTool(filledRecipient);
                }
                floattingText.activate();
            }
            else
            {
                floattingText.activate(requiredToolName);
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
            floattingText.desactivate();
        }
    }

}

using UnityEngine;

public class FacingWaterZone : MonoBehaviour
{
    [SerializeField]
    string requiredToolName;

    [SerializeField]
    FloattingText floattingText;

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

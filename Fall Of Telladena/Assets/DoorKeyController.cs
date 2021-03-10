/*
 * Authors : Manon
 */

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NameAmountPair
{
    public string name;
    public int amount;
}

public class DoorKeyController : DoorController
{
    string id = "MazeDoorInOutsideStairs";
    bool isInContact = false;
    GameObject player = null;

    //[SerializeField]
    //FloattingText floattingText;

    [SerializeField]
    List<NameAmountPair> requiredToolsName = new List<NameAmountPair>();
    [SerializeField]
    string actionName = "Open";

    [SerializeField]
    bool consumeRequiredTools = false;

    private void Start()
    {
        if (Inventory.instance.objectsThatHaveBeenConsumed.Contains(id))
        {
            requiredToolsName.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZone") || other.CompareTag("ContactZoneBrambles"))
        {
            Debug.Log("Contact");
            isInContact = true;
            if (player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }

            //floattingText.activate(hasRequiredTool());
            player.GetComponent<ToolsManager>().ActivateActionInfo(actionName, null, requiredToolsName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone") || other.CompareTag("ContactZoneBrambles"))
        {
            isInContact = false;
            //floattingText.desactivate();
            player.GetComponent<ToolsManager>().DeactivateActionInfo();
        }
    }

    string hasRequiredTool()
    {
        string missingItem = "";
        foreach(NameAmountPair item in requiredToolsName)
        {
            if (!Inventory.instance.HasTool(item.name, item.amount))
            {
                missingItem = item.name;
            }
        }
        return missingItem;
        
    }

    void Update()
    {
        if (isInContact)
        {
            //string missingTool = hasRequiredTool();
            int missingTool = player.GetComponent<ToolsManager>().HasRequiredTools(requiredToolsName);
            player.GetComponent<ToolsManager>().ActivateActionInfo(actionName, null, requiredToolsName);
            //floattingText.activate(missingTool);
            if (missingTool == -1 && Input.GetButtonDown("Action") && !player.GetComponent<ToolsManager>().usingATool)
            {
                player.GetComponent<ToolsManager>().StartCoroutine("UseTool");
                if (consumeRequiredTools)
                {
                    foreach (NameAmountPair item in requiredToolsName)
                    {
                        for(int n = 0; n < item.amount; ++n)
                        {
                            Inventory.instance.RemoveByName(item.name);
                        }
                    }
                    requiredToolsName.Clear();
                    Inventory.instance.objectsThatHaveBeenConsumed.Add(id);
                }
                open();
                player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
                player.GetComponent<ToolsManager>().DeactivateActionInfo();
            }
        }
    }
}

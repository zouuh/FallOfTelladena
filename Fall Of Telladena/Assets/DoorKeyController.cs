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
    bool isInContact = false;
    GameObject player = null;

    [SerializeField]
    FloattingText floattingText;

    [SerializeField]
    List<NameAmountPair> requiredToolsName = new List<NameAmountPair>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            isInContact = true;
            if (player == null)
            {
                player = other.GetComponent<ContactZone>().player;
            }

            floattingText.activate(hasRequiredTool());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            isInContact = false;
            floattingText.desactivate();
        }
    }

    string hasRequiredTool()
    {
        string missingItem = "";
        foreach(NameAmountPair item in requiredToolsName)
        {
            if (!Inventory.instance.hasTool(item.name, item.amount))
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
            string missingTool = hasRequiredTool();
            floattingText.activate(missingTool);
            if (missingTool.Length <= 0 && Input.GetButtonDown("Action"))
            {
                open();
                player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
            }
        }
    }
}

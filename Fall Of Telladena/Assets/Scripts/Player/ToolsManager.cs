/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{
    [SerializeField]
    CanvasController interfaceManager;
    [SerializeField]
    Transform itemZone;

    public bool usingATool = false;
    public bool canDrop = true;

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Action") && canDrop && Inventory.instance.usedItem != null && Inventory.instance.usedItem.droppable && !usingATool)
        {
            Debug.Log("drop");
            itemZone.GetChild(0).transform.Translate(0, 0, -.3f); // avoid bug
            itemZone.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            itemZone.GetChild(0).GetComponent<ItemPickup>().canPickUp = true;
            itemZone.DetachChildren();
            Inventory.instance.Remove(Inventory.instance.usedItem);
            if (Inventory.instance.usedItem == null)
            {
                DeactivateActionInfo();
            }
            else
            {
                CarryItem(true, Inventory.instance.usedItem);
            }
        }
    }

    bool IsUsingRequiredTool(string requiredToolName)
    {
        if (requiredToolName.Length <= 0)
        {
            return true;
        }
        else if (Inventory.instance.isUsingTool(requiredToolName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int HasRequiredTools(List<NameAmountPair> requiredTools)
    {
        if(requiredTools != null)
        {
            for (int i = 0; i < requiredTools.Count; ++i)
            {
                if (!Inventory.instance.HasTool(requiredTools[i].name, requiredTools[i].amount))
                {
                    return i;
                }
            }
        }        
        return -1;
    }

    public void ActivateActionInfo(string actionName, string requiredUsingTool = "", List<NameAmountPair> requiredTools = null)
    {
        int idMissingTool = HasRequiredTools(requiredTools);
        if (idMissingTool != -1)
        {
            interfaceManager.TurnOnActionCanvas(actionName, requiredTools[idMissingTool].name, false);
        }
        else
        {
            if (requiredUsingTool == null)
            {
                interfaceManager.TurnOnActionCanvas(actionName, requiredUsingTool, true);
            }
            else
            {
                interfaceManager.TurnOnActionCanvas(actionName, requiredUsingTool, IsUsingRequiredTool(requiredUsingTool));
            }
        }
    }

    bool IsUsingOneOfTheTools(List<string> tools)
    {
        if(tools == null)
        {
            return true;
        }
        foreach(string tool in tools)
        {
            if (Inventory.instance.isUsingTool(tool))
            {
                return true;
            }
        }
        return false;
    }

    public void ActivateActionInfo(string actionName, List<string> requiredUsingTools, string commonName)
    {
        if (IsUsingOneOfTheTools(requiredUsingTools))
        {
            interfaceManager.TurnOnActionCanvas(actionName+" "+commonName, "", true);
        }
        else
        {
            interfaceManager.TurnOnActionCanvas(actionName, commonName, false);
        }
    }

    public void DeactivateActionInfo()
    {
        if(Inventory.instance.usedItem != null && Inventory.instance.usedItem.droppable && canDrop)
        {
            interfaceManager.TurnOnActionCanvas("Drop");
        }
        else
        {
            interfaceManager.TurnOffActionCanvas();
        }
    }

    public IEnumerator UseTool()
    {
        usingATool = true;
        yield return new WaitForSeconds(1f);
        usingATool = false;
    }

    public void CarryItem(bool carry, Item item = null)
    {
        if (carry)
        {
            if (itemZone.childCount > 0)
            {
                Destroy(itemZone.GetChild(0).gameObject);
            }
            GameObject tmp = Instantiate(item.prefab, itemZone);
            tmp.GetComponent<ItemPickup>().canPickUp = false;
            tmp.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            Destroy(itemZone.GetChild(0).gameObject);
        }
    }
}

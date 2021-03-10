/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** TODO : eau ronces **/
public class ToolsManager : MonoBehaviour
{
    [SerializeField]
    CanvasController interfaceManager;
    [SerializeField]
    Transform itemZone;

    public bool usingATool = false;

    private void PostLateUpdate()
    {
        Debug.Log("Drop ?");
        Debug.Log(Input.GetButtonDown("Action"));
        Debug.Log(Inventory.instance.usedItem != null);
        Debug.Log(Inventory.instance.usedItem.droppable);
        Debug.Log(!usingATool);
        if (Input.GetButtonDown("Action") && Inventory.instance.usedItem != null && Inventory.instance.usedItem.droppable && !usingATool)
        {
            Debug.Log("drop");
            itemZone.GetChild(0).transform.Translate(0, 0, -.1f); // avoid bug
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

    public void DeactivateActionInfo()
    {
        if(Inventory.instance.usedItem != null && Inventory.instance.usedItem.droppable)
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
        Debug.Log("coroutine");
        yield return new WaitForSeconds(1f);
        Debug.Log("coroutine end");
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

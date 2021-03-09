using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** TODO : portes eau ronces **/
public class ToolsManager : MonoBehaviour
{
    [SerializeField]
    CanvasController interfaceManager;
    [SerializeField]
    Transform itemZone;

    public bool usingATool = false;

    private void LateUpdate()
    {
        if(Input.GetButtonDown("Action") && Inventory.instance.usedItem != null && Inventory.instance.usedItem.droppable && !usingATool)
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

    bool HasRequiredTool(string requiredToolName)
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

    public void ActivateActionInfo(string actionName, string requiredToolName = "")
    {
        if(requiredToolName == null)
        {
            interfaceManager.TurnOnActionCanvas(actionName, requiredToolName, true);
        }
        else
        {
            interfaceManager.TurnOnActionCanvas(actionName, requiredToolName, HasRequiredTool(requiredToolName));
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

    public void CarryItem(bool carry, Item item)
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
            Destroy(itemZone.GetChild(0));
        }
    }
}

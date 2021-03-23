/*
 * Authors : Amélia, Manon
 */

using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;			// Reference to the Icon image
	public Button removeButton;	// Reference to the remove button
	public Text amountText;     // Reference to the Amount Text
	public Text nameText;// Reference to the name Text
	public Text descriptionText;// Reference to the descrition Text

	Item item;  // Current item in the slot

	[SerializeField]
	CanvasController canvasController; // needed to manage action infos when using a tool
	[SerializeField]
	ToolsManager toolsManager; // needed to display the item in Oksusu arms

	// Add item to the slot
	public void AddItem (Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
		PrintAmount(item);

	}

	// Print Amount text
	public void PrintAmount(Item item){
		if (item.amount > 0){
			amountText.text = item.amount.ToString();
		}
		else {
			amountText.text = " ";
		}
	}

	// Clear the slot
	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
		amountText.text = " ";
	}

	// Called when the remove button is pressed
	public void OnRemoveButton ()
	{
		if (item.amount == 1){
			item.amount = item.amount-1;
			Inventory.instance.RemoveAll(item);
			nameText.text = "";
			descriptionText.text = "";
		}
		
		else {
			item.amount = item.amount-1;
			PrintAmount(item);
		}
	}

	// Called when the item is pressed
	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
			nameText.text = item.name;
			descriptionText.text = item.description;
            /*if (item.droppable)
			{
				canvasController.TurnOnActionCanvas("Poser");
			}
			toolsManager.CarryItem(true, item);*/
		}
		else {
			/*Inventory.instance.usedItem = null; // no item is used
			canvasController.TurnOffActionCanvas();
			toolsManager.CarryItem(false);*/
			nameText.text = "";
			descriptionText.text = "";
		}
	}


	// Called when the item is pressed
	public void ChangeItem ()
	{
		//Debug.Log("Pressed middle click.");
		if (item != null)
		{
			if (item.activeInstance == true){
				item.activeInstance = false;
			}
			else {
				item.activeInstance = true;
			}
		}
	}

}
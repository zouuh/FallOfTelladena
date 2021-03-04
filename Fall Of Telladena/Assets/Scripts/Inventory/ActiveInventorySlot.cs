using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class ActiveInventorySlot : MonoBehaviour {

	public Image icon;			// Reference to the Icon image
	public Button removeButton;	// Reference to the remove button
	public Text amountText;		// Reference to the Amount Text
	Item item;  // Current item in the slot

	// Add item to the slot
	public void AddItem (Item newItem)
	{
		Debug.Log("AddItem");
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
		PrintAmount(item);

	}

	// Print Amount text
	public void PrintAmount(Item item){
		if (item.amount > 1){
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
			Inventory.instance.Remove(item);
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
		}
	}

}
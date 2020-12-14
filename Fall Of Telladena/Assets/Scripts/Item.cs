using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
  public bool inPipe;
  public bool authorizedInPipe;
  public string itemName = "badItem";

	new public string name = "New Item";	// Name of the item
	public int amount = 0;					// Amount of items
	public Sprite icon = null;				// Item icon
	public bool showInInventory = true;

	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		// Use the item
		// Something may happen
		Debug.Log("Using : " + name);
	}

	// Call this method to remove the item from inventory
	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
}

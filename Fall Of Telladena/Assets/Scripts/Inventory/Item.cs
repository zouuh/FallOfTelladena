/*
 * Authors : Amélia, Manon
 */

using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	new public string name = "New Item";    // Name of the item
	public int amount = 0;                  // Amount of items
	public Sprite icon = null;              // Item icon
	public bool showInInventory = true;
	public bool droppable = false;
	public GameObject prefab;

	// the item the player is using now
	//public bool isUsed = false;

	// Called when the item is pressed in the inventory
	public virtual void Use()
	{
		// Use the item
		// Something may happen
		Debug.Log("Using : " + name);

		// Tell the inventory we are using this item now
		Inventory.instance.usedItem = this;
		// Change the active bar to fit the new configuration
		// ...
	}

	// Call this method to remove the item from inventory
	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this);
	}
}

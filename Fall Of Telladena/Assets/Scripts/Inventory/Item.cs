/*
 * Authors : Amélia, Manon
 */

using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
	new public string name = "New Item";    // Name of the item
	public int amount = 0;              	// Amount of items
	public Sprite icon = null;              // Item icon
	public bool showInInventory = true;
	public bool activeInstance = false;
	public bool droppable = false;
	public bool eatable = false;
	public float energy = 0;
	public GameObject prefab;
	public string description = "";

	// Called when the item is pressed in the inventory
	public virtual void Use()
	{
		// Use the item
		// Something may happen
		//Debug.Log("Using : " + name);

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

	public void eatItem (PlayerMovement playerMov) {
		if (this.eatable) {
			playerMov.energy += this.energy;
			Inventory.instance.Remove(this);
		}
	}
}
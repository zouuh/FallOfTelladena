/*
 * Authors : Amélia, Manon
 */

using System.Collections.Generic;

using System;
using System.Linq;

using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake ()
	{
		instance = this;
	//	Debug.Log("items " + items.Count);
	//	Debug.Log("!= 0");
	//	Debug.Log(InventoryUI.inventory);
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 42;	// Amount of item spaces

	// Our current list of items in the inventory
	public List<Item> items = new List<Item>();

	public string usedItem = ""; // the item the player is using now

	// Add a new item if enough room
	public bool Add (Item item)
	{
		if (item.showInInventory) {
			if (items.Count >= space) {
				Debug.Log ("Not enough room.");
				return false;
			}
			for (int i = 0; i < items.Count; i++){
				Debug.Log("items[i].name : "+ items[i].name);
				Debug.Log("item.name : "+ item.name);
				if(items[i].name == item.name){
					items[i].amount += 1;
					items.Remove(item);
				}
			}
			if(0 == item.amount){
				item.amount = 1;
				items.Add (item);
			}
			
			else {
				items.Add (item);
			}
			
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
		return true;
	}

	// Remove an item
	public void Remove (Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public bool isUsingTool(string toolName)
    {
		if(usedItem == toolName)
        {
			return true;
        }
		return false;
    }


}
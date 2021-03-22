/* 
 * Authors : Amélia, Manon, Zoé
 */

using System.Collections;
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

	public Item usedItem = null; // the item the player is using now

	public List<string> objectsThatHaveBeenConsumed = new List<string>(); // keep track of the objects that have consume objects and that can now be used forever

	// Add a new item if enough room
	public bool Add (Item item)
	{
		if (item.showInInventory) {
			if (items.Count >= space) {
				Debug.Log ("Not enough room.");
				return false;
			}
			// for first appearance
			for (int i = 0; i < items.Count; i++){
				if(items[i].name == item.name){
					items[i].amount += 1;
					items.Remove(item);
				}
			}
			if(item.amount <= 0){
				item.amount = 1;
				items.Add(item);
			}
			else{
				items.Add(item);
			}
			
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
		return true;
	}

	public bool Add(Item item, int n) {
		for(int i=0; i<n; i++) {
			Add(item);
		}
		return true;
	}

	// Remove an item
	public void Remove(Item item)
	{
		// items.Remove(item);
		//Item tmpItem = items.Find(el => el.name.Equals(item.name));
        if (item != null)
        {
			--item.amount;
			if(item.amount <= 0)
            {
				items.Remove(item);
			}
			Debug.Log("New amount ="+item.amount);
		}

		if(usedItem != null && usedItem.amount <= 0)
        {
			// The item used as been removed
			usedItem = null;
        }

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	// Remove an item by name
	public void RemoveByName(string name)
	{
		//items.Remove(items.Find(el => el.name.Equals(name)));
		Item tmpItem = items.Find(el => el.name.Equals(name));
		if (tmpItem != null)
		{
			--tmpItem.amount;
			if (tmpItem.amount <= 0)
			{
				items.Remove(tmpItem);
			}
		}

		if (!items.Exists(el => el.name.Equals(usedItem)))
		{
			// The item used as been removed
			usedItem = null;
		}

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}


	// Remove an item
	public void RemoveAll(Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public bool isUsingTool(string toolName)
	{
		if (usedItem != null && usedItem.name == toolName)
		{
			return true;
		}
		return false;
	}
	public void Remove(Item item, int n) {
		for(int i=0; i<n; i++) {
			Remove(item);
		}
	}

	public bool HasTool(string toolName, int amount)
	{
		/*
		foreach (Item item in items)
        {
			if (item.name == toolName && item.amount >= amount)
			{
				return true;
			}
		}
		*/
		Item result = items.Find(el => el.name == toolName && el.amount >= amount);
		return (result != null ? true : false);
	}

	public void ChangeActiveTool(Item newTool)
    {
        if (HasTool(newTool.name, 1))
		{
			usedItem = newTool;
		}
    }


}
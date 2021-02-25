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
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 42;	// Amount of item spaces

	// Our current list of items in the inventory
	public List<Item> items = new List<Item>();

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

	// Inventory manager
	public bool CheckItem(string name, int amount){
			for (int i = 0; i < items.Count; i++){
				if((items[i].name == name) && (items[i].amount == amount)){
					return true;
				}
			}
		return false;
	
	}

	// Remove an item
	public void Remove (Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}


}
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

	public int space = 10;	// Amount of item spaces

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
				if(items[i] == item){
					Debug.Log("LALALALALALALALALALALALAL");
				}
			}
			items.Add (item);

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
		PrintListItems();
		return true;
	}

	// Remove an item
	public void Remove (Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public void PrintListItems(){
		Debug.Log(items.Count);
		for (int i = 0; i < items.Count; i++)
		{
			Debug.Log("Element : " + items[i].name);
		}
		var dict = items.Select((s, i) => new { s, i }).ToDictionary(x => x.i, x => x.s);
		Debug.Log("Dictionnaire : " + dict);

		foreach (KeyValuePair<int, Item> kvp in dict)
		{
			//textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
			Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
		}

		return;
	}

}

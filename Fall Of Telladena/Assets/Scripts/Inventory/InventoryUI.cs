using UnityEngine;
using UnityEditor;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;  // The entire UI
	public CanvasController cv;

	public static Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots

	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
#if UNITY_EDITOR
		string[] guids2 = AssetDatabase.FindAssets("", new[] {"Assets/Items"});
        foreach (string guid2 in guids2)
        {
			Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid2));			
			foreach (Item o in data)
			{
				if (o.amount >= 1){
					o.amount--;
					inventory.Add(o);
				}
			}
        }
#endif

	}
	
	void Update () {
		// Check to see if we should open/close the inventory
		if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
		UpdateUI();
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void UpdateUI ()
	{
		// Loop through all the slots
		int j = 0;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].ClearSlot();
			if ((i < inventory.items.Count)&&(inventory.items[i].activeInstance == false))	// If there is an item to add
			{
				slots[j].AddItem(inventory.items[i]);	// Add it
			} else
			{
				j--;
			}
			j++;
		
		} 	


		return;

	}



}
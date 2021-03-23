using UnityEngine;
using UnityEditor;

public class ActiveInventoryUI : MonoBehaviour
{

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;  // The entire UI

	public static Inventory inventory;	// Our current inventory

	ActiveInventorySlot[] slots;	// List of all the slots

	void Start () {
        
		inventory = Inventory.instance;
		//inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<ActiveInventorySlot>();
		string[] guids2 = AssetDatabase.FindAssets("", new[] {"Assets/Items"});
		foreach (string guid2 in guids2)
		{
			Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid2));
			if (Inventory.verif == true){
				foreach (Item o in data)
				{
					if (o.amount >= 1){
						inventory.Add(o);
					}
				}
			}
			else {
				foreach (Item o in data)
				{
					if (o.amount >= 1){
						o.amount--;
						inventory.Add(o);
					}
				}
			}
		}
		Inventory.verif = false;
        UpdateActiveUI();
	}
	
	void Update () {
		UpdateActiveUI();
	}
    

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
    
	void UpdateActiveUI ()
	{
		// Loop through all the slots
		int j = 0;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].ClearSlot();
			if ((i < inventory.items.Count)&&(inventory.items[i].activeInstance == true))	// If there is an item to add
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
using UnityEngine;
using UnityEditor;

public class ActiveInventoryUI : MonoBehaviour
{

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;  // The entire UI
	bool verifBtn = true;

	public static Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots

	void Start () {
        
		inventory = Inventory.instance;
		//inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		string[] guids2 = AssetDatabase.FindAssets("", new[] {"Assets/Items"});
        foreach (string guid2 in guids2)
        {
			Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid2));
			Debug.Log(data.Length + " Assets");
			foreach (Item o in data)
			{
				if (o.amount >= 1){
					inventory.Add(o);
				}
			}
        }

        UpdateActiveUI();
/*        */
	}
	
	void Update () {
		// Check to see if we should open/close the inventory
		/*if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
		/*
		if (OnClick.result == true && verifBtn == true){
			for (int i = 0; i < inventory.items.Count; i++)
			{
				if (inventory.items[i].activeInstance == false){
					slots[i].OnRemoveButton ();
				}
				else {
					inventory.items[i].amount -= 1;
				}
			}
		}	
		*/
		UpdateActiveUI();
		//verifBtn = false;
	}
    

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
    
	void UpdateActiveUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if ((i < inventory.items.Count)&&(inventory.items[i].activeInstance == true))	// If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		
		} 
	
        Debug.Log("Updating Active UI");	


		return;

	}



}
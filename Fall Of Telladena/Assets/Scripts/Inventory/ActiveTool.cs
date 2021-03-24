using UnityEngine;
using UnityEditor;

public class ActiveTool : MonoBehaviour
{

    public static Inventory inventory;	// Our current inventory


	void Start () {
        //int  cmpt = 0;
        inventory = Inventory.instance;
#if UNITY_EDITOR
		string[] guids2 = AssetDatabase.FindAssets("", new[] {"Assets/Items"});
        foreach (string guid2 in guids2)
        {
			Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid2));
			Debug.Log(data.Length + " Assets");
			//Debug.Log("data[0] : " + data[0]);
			//FromInventoryToActive(data[0]);
			foreach (Item o in data)
			{
				/*if (o.amount >= 1){
					inventory.Add(o);
				}*/
				o.amount --;
				/*if (cmpt == 0){
					FromInventoryToActive(o);
				}*/
				//cmpt++;
				//Debug.Log(o);
			}
        }  
#endif 
    }

    void FromInventoryToActive(Item item){
		Debug.Log("FromInventoryToActive");
		Debug.Log(item.activeInstance);
		item.activeInstance = true;
		Debug.Log(item.activeInstance);
		//activeInventory.Add(item);
		/*inventory.Remove(item);
		Debug.Log(inventory);
		Debug.Log(activeInventory);*/
	}
}
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //public Transform dest;
    bool isPickedUp = false;
    MovementInput playerMovementInput;

    public Item item;

    void PickUp()
    {
        Debug.Log("PickUP() : " + item.name);
       // Debug.Log("Inventory.instance.items.Count " + OnClick.test);
        // Debug.Log(item);
        bool wasPickedUp = Inventory.instance.Add(item);
        // Debug.Log(isPick);
        if (wasPickedUp)
            Destroy(gameObject);
    }

    void Start()
    {
        playerMovementInput = GameObject.Find("Oksusu").GetComponent<MovementInput>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PickUp();
            isPickedUp = true;
        }
    }
}
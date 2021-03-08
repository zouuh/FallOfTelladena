using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //public Transform dest;
    bool isPickedUp = false;
    MovementInput playerMovementInput;
    ToolsManager toolsManager;
    public bool canPickUp = true;

    public Item item;

    void PickUp()
    {
        Debug.Log("PickUP() : " + item.name);
        // Debug.Log(item);
        bool wasPickedUp = Inventory.instance.Add(item);
        // Debug.Log(isPick);
        if (wasPickedUp)
            Destroy(gameObject);
    }

    void Start()
    {
        playerMovementInput = GameObject.Find("Oksusu").GetComponent<MovementInput>();
        toolsManager = GameObject.Find("Oksusu").GetComponent<ToolsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZone") && canPickUp)
        {
            toolsManager.ActivateActionInfo("Take " + item.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            toolsManager.DeactivateActionInfo();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ContactZone") && Input.GetKeyDown(KeyCode.I) && canPickUp)
        {
            Debug.Log("JE PICK UP");
            PickUp();
            isPickedUp = true;
            toolsManager.DeactivateActionInfo();
        }
    }
}

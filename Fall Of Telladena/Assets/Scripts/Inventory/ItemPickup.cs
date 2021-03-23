/* 
 * Authors : Amélia, Manon, Zoé
 */

using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    //public Transform dest;
    bool isPickedUp = false;
    MovementInput playerMovementInput;
    ToolsManager toolsManager;
    public bool canPickUp = true;
    bool isInContact;
    public Item item;
    public Animator oksusuAnimator;

    public ButtonController ActivateButton = null;

    void PickUp()
    {
        Debug.Log("PickUP() : " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        oksusuAnimator.SetBool("pickUp", true);
        StartCoroutine(WaitForAnim(wasPickedUp));
    }

    IEnumerator WaitForAnim(bool wasPickedUp) {
        yield return new WaitForSeconds(1f);
        if (ActivateButton != null)
        {
            ActivateButton.nbOfColliders--;
            ActivateButton.CheckOpen();
        }
        if (wasPickedUp) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerMovementInput = GameObject.Find("Oksusu").GetComponent<MovementInput>();
        toolsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ToolsManager>();
        oksusuAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZone") && canPickUp)
        {
            toolsManager.canDrop = false;
            toolsManager.ActivateActionInfo("Take "+ item.name, 1, null);
            isInContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZone"))
        {
            toolsManager.canDrop = true;
            toolsManager.DeactivateActionInfo();
            isInContact = false;
        }
    }

    private void Update()
    {
        if (isInContact && Input.GetButtonDown("Action") && canPickUp && !toolsManager.usingATool)
        {
            Debug.Log("JE PICK UP");
            toolsManager.StartCoroutine("UseTool");
            PickUp();
            isPickedUp = true;
            toolsManager.canDrop = true;
            toolsManager.DeactivateActionInfo();
        }
    }
    /*
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ContactZone") && Input.GetKeyDown(KeyCode.I) && canPickUp)
        {
            Debug.Log("JE PICK UP");
            PickUp();
            isPickedUp = true;
            toolsManager.DeactivateActionInfo();
            toolsManager.StartCoroutine("UseTool");
        }
    }
    */
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEntry : MonoBehaviour
{
    bool hasAnObject = false;
    [SerializeField]
    float timer = 1f; // in seconds
    GameObject spawnee;
    public Transform exitPos;

    public AudioManager audioManager;

    ToolsManager toolsManager;
    bool isInContact = false;
    [SerializeField]
    List<string> requiredUsingTools = new List<string>() { "Pièce Cerlce", "Pièce Diamant", "Pièce Hexagone", "Pièce Pentagone", "Pièce Rectangle", "Pièce Carré", "Pièce Etoile", "Pièce Trapèze", "Pièce Triangle" };

    private void Update()
    {
        if(Input.GetButtonDown("Action") && isInContact && !hasAnObject && !toolsManager.usingATool)
        {
            toolsManager.UseTool();

            // Animation
            toolsManager.GetComponent<Animator>().SetBool("pickUp", true);

            StartCoroutine("ItemInPipe");

            Inventory.instance.Remove(Inventory.instance.usedItem);
            if (Inventory.instance.usedItem == null)
            {
                toolsManager.CarryItem(false);
            }
            toolsManager.ActivateActionInfo("Poser", requiredUsingTools, "un item");
        }
    }

    IEnumerator ItemInPipe()
    {
        hasAnObject = true;

        spawnee = Inventory.instance.usedItem.prefab;

        // play a sound
        audioManager.Play("pipeSound");

        yield return new WaitForSeconds(timer);

        var tmp = Instantiate(spawnee, exitPos.position, spawnee.transform.rotation);

        // stop sound
        audioManager.Stop("pipeSound");

        tmp.SetActive(true);
        tmp.GetComponent<Rigidbody>().isKinematic = false;
        tmp.GetComponent<ItemPickup>().canPickUp = true;
        spawnee = null;
        hasAnObject = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContactZoneNests"))
        {
            if (toolsManager == null)
            {
                toolsManager = other.GetComponent<ContactZone>().player.GetComponent<ToolsManager>();
            }

            isInContact = true;
            toolsManager.canDrop = false;

            toolsManager.ActivateActionInfo("Poser", requiredUsingTools, "un item");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ContactZoneNests"))
        {

            isInContact = false;
            toolsManager.canDrop = true;
            toolsManager.DeactivateActionInfo();
        }
    }
}

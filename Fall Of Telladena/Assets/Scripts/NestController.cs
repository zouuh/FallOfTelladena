using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestController : MonoBehaviour
{
    public Symbol mySymbol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Egg"))
        {
            // Position Egg in center
            other.gameObject.transform.parent = this.transform;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.transform.position = this.transform.position;
            other.gameObject.transform.rotation = this.transform.rotation;

            // Change Symbol aspect according to egg color
            mySymbol.changeColor(other.gameObject.GetComponent<ItemMaze>().itemName);
        }
    }
    private void OnTriggerExit(Collider other) // turn off symbol when removing the egg
    {
        if (other.CompareTag("Egg"))
        {
            // Position Egg in center
            other.gameObject.transform.parent = null;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Change Symbol aspect according to egg color
            mySymbol.setDefaultColor();
        }
    }
}

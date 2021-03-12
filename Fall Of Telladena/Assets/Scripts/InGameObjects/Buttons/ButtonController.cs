/*
 * Authors : Manon
 */

using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool isOn = false;
    public Vector3 positionUp = new Vector3(0, 1, 0);
    public Vector3 positionDown = new Vector3(0, 0.3f, 0);
    public GameObject ButtonToPush;

    public DoorController[] doorsToControl;
    public int nbOfColliders = 0;

    public float switchActivationWeight = 0.01f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ButtonBase") && ((other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > switchActivationWeight) || other.CompareTag("ContactZone")))
        {
            ++nbOfColliders;

            foreach(DoorController door in doorsToControl)
            {
                door.open();
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (nbOfColliders < 0 && !other.CompareTag("ButtonBase") && ((other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > switchActivationWeight) || other.CompareTag("ContactZone")))
        {
            --nbOfColliders;

            foreach (DoorController door in doorsToControl)
            {
                door.close();
            }

            //nbOfColliders = 0;
        }
    }
}

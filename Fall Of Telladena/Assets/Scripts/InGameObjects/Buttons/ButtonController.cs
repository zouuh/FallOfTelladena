/*
 * Authors : Manon
 */

using UnityEngine;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    public bool isOn = false;
    public Vector3 positionUp = new Vector3(0, 1, 0);
    public Vector3 positionDown = new Vector3(0, 0.3f, 0);
    public GameObject ButtonToPush;

    public DoorController[] doorsToControl;
    List<GameObject> colliders = new List<GameObject>();
    public int nbOfColliders = 0;

    public float switchActivationWeight = 0.01f;

    private void Update()
    {
        if(colliders.Count > 0)
        {
            RemoveNull();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ButtonBase") && ((other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > switchActivationWeight) || other.CompareTag("ContactZone")))
        {
            ++nbOfColliders;
            colliders.Add(other.gameObject);

            foreach(DoorController door in doorsToControl)
            {
                door.open();
            }
        }
        RemoveNull();
    }

    void OnTriggerExit(Collider other)
    {
        if (nbOfColliders > 0 && !other.CompareTag("ButtonBase") && ((other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > switchActivationWeight) || other.CompareTag("ContactZone")))
        {
            --nbOfColliders;
            colliders.Remove(other.gameObject);
             
            if(nbOfColliders <= 0)
            {
                foreach (DoorController door in doorsToControl)
                {
                    door.close();
                }
            }

            //nbOfColliders = 0;
        }
        RemoveNull();
    }

    void RemoveNull()
    {
        var tmp = colliders.FindAll(el => el == null);
        if (tmp.Count > 0)
        {
            foreach (GameObject obj in tmp)
            {
                colliders.Remove(obj);
            }
            if (colliders.Count <= 0)
            {
                foreach (DoorController door in doorsToControl)
                {
                    door.close();
                }
            }
        }
    }
}

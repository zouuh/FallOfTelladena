using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    Quaternion initialRotation = new Quaternion(0, 0, 0, 1);

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = initialRotation;
    }
}

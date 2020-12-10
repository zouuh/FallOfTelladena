﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public Transform resetPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // begin transition
            // ...

            // begin teleportation
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = resetPos.transform.position;
            other.gameObject.transform.rotation = resetPos.transform.rotation;

            // reset platforms
            // ...

            // end transition
            // ...

            // end teleportation
            other.gameObject.SetActive(true);

        }
    }
}

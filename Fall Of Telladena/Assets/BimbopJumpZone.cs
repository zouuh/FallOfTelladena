using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BimbopJumpZone : MonoBehaviour
{
    public int jumpCount;
    public bool isInZone = false;
    [SerializeField]
    float timeBetweenEachJump = 5f; // in seconds
    public float timer = 0f;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        timer = timeBetweenEachJump;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
        }
    }


}

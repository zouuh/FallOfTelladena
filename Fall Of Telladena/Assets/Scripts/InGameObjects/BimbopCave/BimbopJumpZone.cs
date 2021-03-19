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
    [SerializeField]
    int requiredJumpCount = 10;
    public bool caveIsOpen = false;

    [SerializeField]
    BimbopCave cave;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void AddJump()
    {
        ++jumpCount;
        if (jumpCount >= requiredJumpCount)
        {
            cave.Appear();
            caveIsOpen = true;
        }
    }

    public void ResetJump()
    {
        jumpCount = 0;
    }

    public void ResetTimer()
    {
        timer = timeBetweenEachJump;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Is in zone !");
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

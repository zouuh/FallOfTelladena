using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    bool playerIsOnLadder = false;
    GameObject player;
    Vector3 initPos;
    float maxY = 3.5f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        initPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Is on ladder !");
            playerIsOnLadder = true;
            other.transform.parent = transform;
            //other.GetComponent<CharacterController>().enabled = false; // Player should has a CharacterController
        }
    }
    private void Update()
    {
        if (playerIsOnLadder)
        {
            //float horizontal = Input.GetAxisRaw("Horizontal");

            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                //player.transform.parent = null;
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<PlayerMovement>().enabled = true;
                //playerIsOnLadder = false;
            }
            else
            {
                //float vertical = Input.GetAxisRaw("Vertical");
                if (Input.GetKey(KeyCode.UpArrow)) {
                    player.GetComponent<CharacterController>().enabled = false;
                    player.GetComponent<PlayerMovement>().enabled = false;

                    if(transform.position.y < maxY)
                    {
                        // Move translation along the object's y-axis
                        transform.Translate(0, Time.deltaTime, 0);
                    }
                    else
                    {
                        player.GetComponent<CharacterController>().enabled = true;
                        player.GetComponent<PlayerMovement>().enabled = true;

                        playerIsOnLadder = false;
                        player.transform.parent = null;
                        
                        transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
                    }
                    

                }else if(Input.GetKey(KeyCode.DownArrow))
                {
                    player.GetComponent<CharacterController>().enabled = false;
                    player.GetComponent<PlayerMovement>().enabled = false;

                    if (transform.position.y > initPos.y)
                    {
                        // Move translation along the object's y-axis
                        transform.Translate(0, -Time.deltaTime, 0);
                    }
                    else
                    {
                        player.GetComponent<CharacterController>().enabled = true;
                        player.GetComponent<PlayerMovement>().enabled = true;
                        transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
                    }
                }
            }
        }
        else
        {
            if (transform.position.y < maxY)
            {
                transform.position = new Vector3(initPos.x, initPos.y + player.transform.position.y, initPos.z);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit ladder !");
            playerIsOnLadder = false;
            other.transform.parent = null;
            other.GetComponent<CharacterController>().enabled = true; // Player should has a CharacterController
            transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Camera camera;
    float speed = 3.0f;
    float jumpSpeed = 6.0f;
    float gravity = 20.0f;
    CharacterController characterController;
    //Animator animator;
    Coroutine jump;
    Vector3 movement = Vector3.zero;
    Vector3 input = Vector3.zero;
    Vector2 adjustedInput = Vector2.zero;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (characterController.isGrounded)
        {
            // Playing Running Animation
            //animator.SetBool("Running", (Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow)) | (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow)));
            // Getting Input For Moving Horizontally
            input.x = (InputValue(KeyCode.RightArrow) - InputValue(KeyCode.LeftArrow));
            // Getting Input For Moving Vertically
            input.z = (InputValue(KeyCode.UpArrow) - InputValue(KeyCode.DownArrow));
            // Normalizing Input To Have Normal Diagonal Speed & Speeding
            adjustedInput = new Vector2(input.x, input.z).normalized * speed;
            movement.x = adjustedInput.x;
            movement.z = adjustedInput.y;
            // Starting Jump Coroutine
            if (Input.GetButton("Jump") && jump == null)
            {
                jump = StartCoroutine(Jump());
            }
        }
        else
        {
            movement.y -= gravity * Time.deltaTime;
        }
        // Finally Moving Character Depending On Previously Adjusted Input & Camera
        characterController.Move(Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * (movement * Time.deltaTime));
    }
    IEnumerator Jump()
    {
        // Starting Animation And Waiting Till Legs Bend
        //animator.SetBool("Jumping", true);
        yield return new WaitForSeconds(0.25f);
        // Performing Jump And Waiting Till Animation Finishes
        movement.y = jumpSpeed;
        yield return new WaitForSeconds(0.75f);
        // Jump Finished, Changing Values Back So We Can Jump Again
        jump = null;
        //animator.SetBool("Jumping", false);
    }
    float InputValue(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
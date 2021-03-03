/*
 * Authors : Zoé
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Public attributes
    

    // Private attributes
    private bool slide = false;
    private int jumpCounter = 0;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private float vSpeed = 0;
    private float maxSpeed = 10f;
    private float maxCoef = 1f;
    private float speedCoef = 0;

    private Transform cam;
    private Animator animator;
    private CharacterController controller;
    private GameObject mainVueCanvas;


    void Start() {
        cam = FindObjectOfType<Camera>().transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainVueCanvas = GameObject.FindGameObjectWithTag("Interface").transform.Find("MainInterfaceCanvas").gameObject;
    }

    void FixedUpdate() {
        // Get movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get movement direction 
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Active pick up animation if needed
        if(Input.GetKeyDown("left shift")) {
            animator.SetBool("pickUp", true);
        }

        // Active power animation if needed
        if(Input.GetKeyDown("left alt")) {
            animator.SetBool("usePower", true);
        }

        if(Input.GetKeyDown("tab")) {
            slide = !slide;
        }

        // Only if player isn't picking up item or speaking to PNJ
        if(!animator.GetBool("pickUp") && mainVueCanvas.activeSelf) {
            // If movement necessary
            if(direction.magnitude >= 0.1f) {
                // Get and apply angle of rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // If jump asked
                if(Input.GetButtonDown("Jump")) {
                    // If player is on the floor -> jump, set the animation and move up the collider
                    if(controller.isGrounded) {
                        jumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                    // Else if double jump is possible -> do it again
                    else if(!controller.isGrounded && jumpCounter < 2) {
                        jumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                }
                // If not on the floor ->  bring back to floor and move down the collider
                if(!controller.isGrounded && !animator.GetBool("jump")) {
                    vSpeed -= 0.2f;
                }

                // If jumping -> decrease speed move
                if(animator.GetBool("jump")) {
                    maxSpeed = 5f;
                    vSpeed = 0;
                }
                else {
                    maxSpeed = 10f;
                }

                // Get maxCoef depending on movement mode
                if(slide) {
                    maxCoef = 1.8f;
                }
                else {
                    maxCoef = 1f;
                }

                // Accelerate if not maxSpeed
                if(speedCoef < maxCoef) {
                    speedCoef += 0.05f;
                }
                else if(speedCoef > maxCoef) {
                    speedCoef -= 0.05f;
                }

                // Get and apply move direction and speed
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir.y = vSpeed;
                controller.Move(moveDir.normalized * maxSpeed * speedCoef * Time.deltaTime);

                // Update blendTree animation
                animator.SetFloat("speed", speedCoef);
            }

            // If no movement asked
            else {
                // If jump asked, same as before
                if(Input.GetButtonDown("Jump")) {
                    // If player is on the floor -> jump, set the animation and move up the collider
                    if(controller.isGrounded) {
                        jumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                    // Else if double jump is possible -> do it again
                    else if(!controller.isGrounded && jumpCounter < 2) {
                        jumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                }
                if(!controller.isGrounded && !animator.GetBool("jump")) {
                    vSpeed -= 0.2f;
                }
                else {
                    vSpeed = 0;
                }
                // Move on Y axe
                controller.Move(new Vector3(0, vSpeed * 0.5f * Time.deltaTime, 0));

                // Slow speed if necessary
                if(speedCoef > 0) {
                    speedCoef -= 0.05f;
                }

                // Update blendTree animation
                animator.SetFloat("speed", speedCoef);
            }
        }
    }
}
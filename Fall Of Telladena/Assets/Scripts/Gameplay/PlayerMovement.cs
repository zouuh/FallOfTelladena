//ZOE 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Public attributes
    public GameObject mainVueCanvas;

    // Private attributes
    private int dJumpCounter = 0;
    private int nbOfAlowedDJumps = 0;
    private float turnSmoothVelocity;
    // private float gravity = 9.8f;
    private float maxSpeed = 10;
    private float vSpeed = 0;
    private float speedCoef = 0;
    private float turnSmoothTime = 0.1f;
    // private float jumpSpeed = 8;
    private Transform cam;
    private Animator animator;
    private CharacterController controller;

    void Start() {
        cam = FindObjectOfType<Camera>().transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        // Get movement Input
        float horizontal = Input.GetAxisRaw("Horizontal"); //Remettre en utilisant InputManager
        float vertical = Input.GetAxisRaw("Vertical"); //Remettre en utilisant InputManager

        // Get movement direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Active pick up animation if needed
        if(Input.GetKeyDown("r")) {
            animator.SetBool("pickUp", true);
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
                if (Input.GetButtonDown("Jump")) {
                    // If player on floor, jump and set animation
                    if (controller.isGrounded) {
                        // vSpeed = jumpSpeed;
                        dJumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                    // If not and can double jump, do it again
                    else if (!controller.isGrounded && dJumpCounter < nbOfAlowedDJumps) {
                        // vSpeed = jumpSpeed;
                        dJumpCounter++;
                        animator.SetBool("jump", true);
                    }
                }
                // If no jump asked, don't move on Y axe
                else if(controller.isGrounded) {
                    vSpeed = 0;
                    maxSpeed = 10f;
                }
                // If not on floor, negative movement on Y axe to simulate gravity
                if(!controller.isGrounded) {
                    vSpeed -= 0.2f;
                    maxSpeed = 3f;
                }

                // Get and apply move direction and speed
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir.y = vSpeed;
                controller.Move(moveDir.normalized * maxSpeed * speedCoef * Time.deltaTime);

                // Accelerate if not maxSpeed
                if(speedCoef < 1) {
                    speedCoef += 0.05f;
                }
                // Active run animation by blendTree
                animator.SetFloat("speed", speedCoef);
            }
            // If no movement necessary
            else {
                // If jump, same as before
                if (Input.GetButtonDown("Jump")) {
                    if (controller.isGrounded) {
                        // vSpeed = jumpSpeed;
                        dJumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                    else if (!controller.isGrounded && dJumpCounter < nbOfAlowedDJumps) {
                        // vSpeed = jumpSpeed;
                        dJumpCounter++;
                        animator.SetBool("jump", true);
                    }
                }
                else if(controller.isGrounded) {
                    vSpeed = 0;
                }
                if(!controller.isGrounded) {
                    vSpeed -= 0.2f;
                }
                // Move on Y axe
                controller.Move(new Vector3(0, vSpeed * 0.5f * Time.deltaTime, 0));
                // Slow speed if necessary
                if(speedCoef > 0) {
                    speedCoef -= 0.05f;
                }
                // Active run animation by blendTree
                animator.SetFloat("speed", speedCoef);
            }
        }
    }
}

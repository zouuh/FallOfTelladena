
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public GameObject dialogueCanvas;
    public float speedCoef = 0;
    public float maxSpeed = 10;
    public float jumpSpeed = 20;
    public float gravity = 9.8f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    bool pickUp = false;
    float vSpeed = 0;
    int dJumpCounter = 0;
    int nrOfAlowedDJumps = 0;


    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        pickUp = animator.GetBool("pickUp");

        if(Input.GetKeyDown("r")) {
            pickUp = true;
            animator.SetBool("pickUp", true);
        }

        if(!pickUp && !dialogueCanvas.activeSelf) {
            if(direction.magnitude >= 0.1f) {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if (Input.GetButtonDown("Jump"))
                {
                    if (controller.isGrounded)
                    {
                        vSpeed = jumpSpeed;
                        dJumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                    else if (!controller.isGrounded && dJumpCounter < nrOfAlowedDJumps)
                    {
                        vSpeed = jumpSpeed;
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

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir.y = vSpeed;
                controller.Move(moveDir.normalized * maxSpeed * speedCoef * Time.deltaTime);

                if(speedCoef < 1) {
                    speedCoef += 0.05f;
                }
                animator.SetFloat("speed", speedCoef);
            }
            else {                
                // if (!controller.isGrounded){
                //     vSpeed -= gravity * Time.deltaTime;
                // }

                if (Input.GetButtonDown("Jump"))
                {
                    if (controller.isGrounded)
                    {
                        vSpeed = jumpSpeed;
                        dJumpCounter = 0;
                        animator.SetBool("jump", true);
                    }
                    else if (!controller.isGrounded && dJumpCounter < nrOfAlowedDJumps)
                    {
                        vSpeed = jumpSpeed;
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

                controller.Move(new Vector3(0, vSpeed * 0.5f * Time.deltaTime, 0));
                
                if(speedCoef > 0) {
                    speedCoef -= 0.05f;
                }
                animator.SetFloat("speed", speedCoef);
            }
        }
    }
}

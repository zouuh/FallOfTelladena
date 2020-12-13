
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public float speedCoef = 0;
    public float maxSpeed = 10;
    public float jumpSpeed = 5;
    public float gravity = 9.8f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    bool pickUp = false;
    float vSpeed = 0;

    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        pickUp = animator.GetBool("pickUp");
        if(Input.GetKeyDown("r")) {
            pickUp = true;
            animator.SetBool("pickUp", true);
        }

        if(!pickUp) {
            if(direction.magnitude >= 0.1f) {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if (controller.isGrounded){
                    vSpeed = 0;
                }

                // apply gravity acceleration to vertical speed:
                vSpeed -= gravity * Time.deltaTime;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir.y = vSpeed;
                controller.Move(moveDir.normalized * maxSpeed * speedCoef * Time.deltaTime);

                if(speedCoef < 1) {
                    speedCoef += 0.05f;
                }
                
                animator.SetFloat("speed", speedCoef);
            }
            else {
                if(speedCoef > 0) {
                    speedCoef -= 0.05f;
                }
                animator.SetFloat("speed", speedCoef);
            }
        }
    }
}

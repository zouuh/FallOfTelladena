/* 
 * Authors : Zoé, Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : StateMachineBehaviour
{
    int frame = 0;
    float jumpTime = 0;
    public BimbopJumpZone bimbopJumpZone = null;
    public PlayerPositionManager playerPositionManager = null;
    private CharacterController controller = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        frame = 0;
        jumpTime = 0;

        if(controller == null) {
            controller = FindObjectOfType<CharacterController>();
        }
        if (bimbopJumpZone == null) {
            bimbopJumpZone = FindObjectOfType<BimbopJumpZone>();
        }
        if (playerPositionManager == null) {
            playerPositionManager = FindObjectOfType<PlayerPositionManager>().GetComponent<PlayerPositionManager>();
        }
        playerPositionManager.SaveLastPosition();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        // Do it each frame
        if(jumpTime >= 1f/24f) {
            frame ++;
            jumpTime = 0;

            // Move collider while jumping
            if (frame < 7) {
                controller.center = new Vector3(0, controller.center.y + 0.1f, 0);
            }
            else if (frame > 7 && frame < 18) {
                controller.center = new Vector3(0, controller.center.y - 0.06f, 0);
            }
        }
        // Increase jump time
        jumpTime += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // Update Bimbop cave jump counter
        if (bimbopJumpZone != null && !bimbopJumpZone.caveIsOpen) { // only usefull until cave is open
            if (bimbopJumpZone.isInZone) {
                if (bimbopJumpZone.timer > 0f) {
                    bimbopJumpZone.AddJump();
                }
                else {
                    bimbopJumpZone.ResetJump();
                }
                bimbopJumpZone.ResetTimer();
            }
        }
        
        // Replace collider if necessary
        controller.center = new Vector3(0, 0.65f, 0);
        // Stop jump animation
        animator.SetBool("jump", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

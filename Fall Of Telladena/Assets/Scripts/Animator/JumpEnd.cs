//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : StateMachineBehaviour
{
    private CharacterController controller;
    //public Animation animation;
    int frame = 0;
    float time = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(controller == null) {
            controller = FindObjectOfType<CharacterController>();
        }
        frame = 0;
        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(time >= 1f/24f) {
            frame ++;
            time = 0;

            if (frame < 7) {
                controller.center = new Vector3(0, controller.center.y + 0.1f, 0);
            }
            else if (frame > 7 && frame < 18) {
                controller.center = new Vector3(0, controller.center.y - 0.06f, 0);
            }
        }
        
        time += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.center = new Vector3(0, 0.65f, 0);
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

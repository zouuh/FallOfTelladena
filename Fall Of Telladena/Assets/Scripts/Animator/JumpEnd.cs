//ZOE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : StateMachineBehaviour
{
    public BimbopJumpZone bimbopJumpZone = null;
    public PlayerPositionManager playerPositionManager = null;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bimbopJumpZone == null)
        {
            bimbopJumpZone = FindObjectOfType<BimbopJumpZone>();
        }
        if (playerPositionManager == null)
        {
            playerPositionManager = FindObjectOfType<PlayerPositionManager>().GetComponent<PlayerPositionManager>();
        }
        playerPositionManager.SaveLastPosition();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!bimbopJumpZone.caveIsOpen) // only usefull until cave is open
        {
            if (bimbopJumpZone.isInZone)
            {
                if (bimbopJumpZone.timer > 0f)
                {
                    bimbopJumpZone.AddJump();
                }
                else
                {
                    bimbopJumpZone.ResetJump();
                }
                bimbopJumpZone.ResetTimer();
            }
        }
        
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

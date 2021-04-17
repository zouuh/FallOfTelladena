/* 
 * Authors : Zoé
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenityQuest_State2 : StateMachineBehaviour
{
    StoryManager storyManager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (storyManager == null) {
            storyManager = FindObjectOfType<StoryManager>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Go to next step
        if(storyManager.yoh.HaveSeenDialogue(0) || storyManager.serenityQuestAdvencement >= 2) {
            animator.SetInteger("SerenityQuestAdvencement", 2);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Change Yoh's dialogue ID
        NPC yoh = storyManager.yoh;
        yoh.SetDialogueID(1);
        yoh.SaveNPC();

        // Update story manager
        storyManager.SetSerenityAdvencement(2);
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

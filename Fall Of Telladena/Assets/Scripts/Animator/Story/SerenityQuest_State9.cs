/* 
 * Authors : Zoé
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenityQuest_State9 : StateMachineBehaviour
{
    StoryManager storyManager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       storyManager = FindObjectOfType<StoryManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(Inventory.instance.HasTool("LullubyMushroom", 1) || storyManager.serenityQuestAdvencement >= 9) {
           animator.SetInteger("SerenityQuestAdvencement", 9);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Change Namou's dialogue ID
        NPC namou = storyManager.namou;
        namou.SetDialogueID(0);
        namou.SaveNPC();

        // Change Yoh's dialogue ID
        NPC yoh = storyManager.yoh;
        yoh.SetDialogueID(4);
        yoh.SaveNPC();

        // Change Migwa's dialogue ID
        NPC migwa = storyManager.migwa;
        migwa.SetDialogueID(0);
        migwa.SaveNPC();

        // Update story manager
        storyManager.SetSerenityAdvencement(9);
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

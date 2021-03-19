/* 
 * Authors : Zoé
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuest_State2 : StateMachineBehaviour
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
       if(storyManager.aiki.HaveSeenDialogue(1) || storyManager.mainQuestAdvencement >= 2) {
           animator.SetInteger("MainQuestAdvencement", 2);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Change Aïki's dialogue ID
        NPC aiki = storyManager.aiki;
        aiki.SetDialogueID(2);
        aiki.SaveNPC();


        // Close the crystal room door

        // Begin the 3 stone quests (serenity, clarity and fertility)
        storyManager.beginStoneQuests = true;

        // Update story manager
        storyManager.SetMainAdvencement(2);
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

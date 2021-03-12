using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenityQuest_State7 : StateMachineBehaviour
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
       if(storyManager.migwa.HaveSeenDialogue(1) || storyManager.serenityQuestAdvencement >= 7) {
           animator.SetInteger("SerenityQuestAdvencement", 7);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Add a maze key to the inventory 
        Inventory.instance.Add(storyManager.mazeKey);

        // Change Byoldal's dialogue ID
        NPC byoldal = storyManager.byoldal;
        byoldal.SetDialogueID(1);
        byoldal.SaveNPC();

        // Change Migwa's dialogue ID
        NPC migwa = storyManager.migwa;
        migwa.SetDialogueID(2);
        migwa.SaveNPC();

        // Update story manager
        storyManager.SetSerenityAdvencement(7);
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

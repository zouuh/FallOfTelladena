using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenityQuest_State10 : StateMachineBehaviour
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
       if(storyManager.migwa.HaveSeenDialogue(3) || storyManager.serenityQuestAdvencement >= 10) {
           animator.SetInteger("SerenityQuestAdvencement", 10);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Remove the mushrooms from the inventory 
        Inventory.instance.Remove(storyManager.lullubyMushroom);

        // Add the potion to the inventory 
        Inventory.instance.Add(storyManager.lullubyPotion);

        // Change Yoh's dialogue ID
        NPC yoh = storyManager.yoh;
        yoh.SetDialogueID(5);
        yoh.SaveNPC();

        // Change Migwa's dialogue ID
        NPC migwa = storyManager.migwa;
        migwa.SetDialogueID(4);
        migwa.SaveNPC();

        // Update story manager
        storyManager.SetSerenityAdvencement(10);
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

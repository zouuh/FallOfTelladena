using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenityQuest_State5 : StateMachineBehaviour
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
        Debug.Log("have seen dialogue : " + storyManager.yoh.HaveSeenDialogue(1));
        Debug.Log("has turbull : " + Inventory.instance.HasTool("Turbull", 1));
        if((storyManager.yoh.HaveSeenDialogue(1) && Inventory.instance.HasTool("Turbull", 1)) || storyManager.serenityQuestAdvencement >= 5) {
            Debug.Log("j'ai le turbull");
            animator.SetInteger("SerenityQuestAdvencement", 5);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Change Yoh's dialogue ID
        NPC yoh = storyManager.yoh;
        yoh.SetDialogueID(2);
        yoh.SaveNPC();

        // Change Namou's dialogue ID
        NPC namou = storyManager.namou;
        namou.SetDialogueID(1);
        namou.SaveNPC();

        // Update story manager
        storyManager.SetSerenityAdvencement(5);
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

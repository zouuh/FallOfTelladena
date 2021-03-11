using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1_MainQuest : StateMachineBehaviour
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
       if(storyManager.inCrystalRoom == 1) {
           animator.SetInteger("MainQuestAdvencement", 1);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Moves Aïki and change his dialogue ID
        NPC aiki = storyManager.aiki;
        aiki.SetScene("OutsideCastle");
        aiki.SetPosition(new Vector3(-20f,-10.138f, -10f));
        aiki.SetDialogueID(1);
        aiki.SaveNPC();

        // Yoh appears
        NPC yoh = storyManager.yoh;
        yoh.SetScene("Village");
        yoh.SaveNPC();
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

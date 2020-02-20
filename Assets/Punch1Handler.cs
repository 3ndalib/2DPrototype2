using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch1Handler : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //public GameObject Player;
    //public CombatController CC; 

    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{     
    //    Player = GameObject.Find("Player");
    //    CC = Player.GetComponent<CombatController>();
    //    CC.DelayTimer = CC.TimerValue;
    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    CC.DelayTimer -= Time.deltaTime;
    //}

    //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (Input.GetMouseButtonDown(0) && CC.DelayTimer > 0)
    //    {
    //        animator.SetBool("Punch1", false);
    //        animator.SetBool("Punch2", true);
    //    }
    //    else if (Input.GetMouseButtonDown(1) && CC.DelayTimer > 0) 
    //    {
    //        animator.SetBool("Punch1", false);
    //        animator.SetBool("Kick2", true);
    //    }
    //}

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

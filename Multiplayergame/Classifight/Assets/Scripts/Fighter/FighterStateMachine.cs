using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStateMachine : StateMachineBehaviour
{
    public FighterState behaviorState;

    public float horizontalForce;
    public float verticalForce;

    protected Fighter fighter;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(fighter == null)
        {
            fighter = animator.gameObject.GetComponent<Fighter>();
        }
        fighter.currentState = behaviorState;
        fighter.body.AddRelativeForce(new Vector2(0, verticalForce));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter.body.AddRelativeForce(new Vector2(horizontalForce, verticalForce));
    }
}

using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateLanding : CatState
{
    CatController cat;
    public void Enter(CatController cat)
    {
    }
    public void changeState(TrackEntry trackEntry)
    {
        cat.stateMachine.ChangeState(CatStateID.Idle);
    }
    public void Exit(CatController cat)
    {
    }

    public void FixedUpdateInState(CatController cat)
    {
    }

    public CatStateID GetStateID()
    {
        return CatStateID.Landing;
    }

    public void UpdateInState(CatController cat)
    {
        if (cat.controller.skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name.Equals("Idle"))
        {
            cat.stateMachine.ChangeState(CatStateID.Idle);
        }
    }
}

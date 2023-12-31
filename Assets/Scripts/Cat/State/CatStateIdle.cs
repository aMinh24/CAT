
using UnityEngine;


public class CatStateIdle : CatState
{
    public void Enter(CatController cat)
    {
        cat.rb.sharedMaterial = null;
    }

    public void Exit(CatController cat)
    {
    }
    public CatStateID GetStateID()
    {
        return CatStateID.Idle;
    }

    public void FixedUpdateInState(CatController cat)
    {
        
    }


    public void UpdateInState(CatController cat)
    {
        if (cat.inputMove.x != 0)
        {
            cat.stateMachine.ChangeState(CatStateID.Move);
        }
        if (cat.isJumping)
        {
            cat.stateMachine.ChangeState(CatStateID.Jump);
        }
        if (!cat.IsGround())
        {
            cat.stateMachine.ChangeState(CatStateID.InAir);
        }
    }
}

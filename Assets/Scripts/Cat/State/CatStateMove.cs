
using UnityEngine;

public class CatStateMove : CatState
{
    private float x = 0;
    public void Enter(CatController cat)
    {
        cat.rb.sharedMaterial = null;
        cat.isClimbing = false;
    }

    public void Exit(CatController cat)
    {
    }
    public CatStateID GetStateID()
    {
        return CatStateID.Move;
    }

    public void FixedUpdateInState(CatController cat)
    {
        
        x = cat.inputMove.x * cat.speed;
        //if (x != 0)
        //{
        //    cat.animator.SetBool("isWalking", true);
        //}
        //else
        //{
        //    cat.animator.SetBool("isWalking", false);
        //}
        cat.rb.velocity = new Vector2(x, cat.rb.velocity.y);
    }


    public void UpdateInState(CatController cat)
    {
        if (cat.inputMove.x == 0)
        {
            cat.stateMachine.ChangeState(CatStateID.Idle);
        }
        if (cat.isJumping)
        {
            cat.stateMachine.ChangeState(CatStateID.Jump);
        }
    }
}

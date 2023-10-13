using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateClimbing : CatState
{
    public void Enter(CatMovement cat)
    {
        cat.rb.gravityScale = 0;
        cat.rb.velocity = Vector2.zero; 
        cat.isClimbing = true;
        Debug.Log("climb");
    }

    public void Exit(CatMovement cat)
    {
        cat.rb.gravityScale = 3;
        cat.isClimbing = false;
    }

    public CatStateID GetStateID()
    {
        return CatStateID.Climbing;
    }
    public void FixedUpdateInState(CatMovement cat)
    {
        float y = cat.inputMove.y * cat.speedClimb;
        float x = cat.inputMove.x;
        if (cat.isJumping && (cat.spriteRenderer.flipX ? (x > 0) : (x < 0)))
        {
            cat.stateMachine.ChangeState(CatStateID.InTheAir);
        }
        else if (cat.isJumping)
        {
            cat.isJumping = false;
        }
        if (cat.IsGround())
        {
            cat.stateMachine.ChangeState(CatStateID.OnGround);
        }
        cat.rb.velocity = new Vector2(0, y - cat.speedSlide);
    }


    public void UpdateInState(CatMovement cat)
    {
        
    }
}

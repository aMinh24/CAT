using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateClimbing : CatState
{
    public void Enter(CatController cat)
    {
        cat.rb.gravityScale = 0;
        cat.rb.velocity = Vector2.zero; 
        cat.isClimbing = true;
        Debug.Log("climb");
    }

    public void Exit(CatController cat)
    {
        cat.rb.gravityScale = 3;
        cat.isClimbing = false;
    }

    public CatStateID GetStateID()
    {
        return CatStateID.Climbing;
    }
    public void FixedUpdateInState(CatController cat)
    {
        float y = cat.inputMove.y * cat.speedClimb;
        float x = cat.inputMove.x;
        if (cat.isJumping && (cat.transform.rotation.y !=0 ? (x > 0) : (x < 0)))
        {
            cat.stateMachine.ChangeState(CatStateID.Jump);
        }
        else if (cat.isJumping)
        {
            cat.isJumping = false;
        }
        if (cat.IsGround())
        {
            if (cat.inputMove.x != 0)
            {
                cat.stateMachine.ChangeState(CatStateID.Idle);
            }
            
        }
        cat.rb.velocity = new Vector2(0, y - cat.speedSlide);
    }


    public void UpdateInState(CatController cat)
    {
        
    }
}

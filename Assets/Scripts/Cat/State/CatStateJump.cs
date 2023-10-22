using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateJump : CatState
{
    private bool flag;
    private bool change;
    private float x = 0;
    public void Enter(CatController cat)
    {
        cat.rb.sharedMaterial = DataManager.Instance.Config.slip;
        flag = true;
        change = false;
        //cat.rb.velocity = new Vector2(cat.rb.velocity.x*(cat.speedJump/10), cat.rb.velocity.y+ cat.jumpHeight);
    }
    public void Exit(CatController cat)
    {
        cat.isJumping = false;
    }
    public CatStateID GetStateID()
    {
        return CatStateID.Jump;
    }

    public void FixedUpdateInState(CatController cat)
    {
        x = cat.inputMove.x * cat.speed;
        if (!flag)
            cat.rb.velocity = new Vector2(x * cat.speedJump / 10, cat.rb.velocity.y);
        if (cat.IsGround())
        {
            if (change)
                cat.stateMachine.ChangeState(CatStateID.Idle);
            change = true;
        }
        else
        {
            change = false;
        }
        if (cat.IsClimbing())
        {
            cat.stateMachine.ChangeState(CatStateID.Climbing);
        }
    }


    public void UpdateInState(CatController cat)
    {
        if (flag)
        {
            cat.rb.velocity = new Vector2(cat.rb.velocity.x * (cat.speedJump / 10), cat.rb.velocity.y + cat.jumpHeight);
            flag = false;
        }
    }
}

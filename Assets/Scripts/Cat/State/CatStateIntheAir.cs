using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateIntheAir : CatState
{
    private bool flag;
    private bool change;
    private float x = 0;
    public void Enter(CatMovement cat)
    {
        cat.rb.sharedMaterial = DataManager.Instance.Config.slip;
        flag = true;
        change = false;
        Debug.Log("Jump");
        //cat.rb.velocity = new Vector2(cat.rb.velocity.x*(cat.speedJump/10), cat.rb.velocity.y+ cat.jumpHeight);
    }
    public void Exit(CatMovement cat)
    {
        cat.isJumping = false;
    }
    public CatStateID GetStateID()
    {
        return CatStateID.InTheAir;
    }

    public void FixedUpdateInState(CatMovement cat)
    {
        x = cat.inputMove.x * cat.speed;
        if (!flag)
            cat.rb.velocity = new Vector2(x * cat.speedJump / 10, cat.rb.velocity.y);
        if (cat.IsGround())
        {
            if (change)
                cat.stateMachine.ChangeState(CatStateID.OnGround);
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


    public void UpdateInState(CatMovement cat)
    {
        if (flag)
        {
            cat.rb.velocity = new Vector2(cat.rb.velocity.x * (cat.speedJump / 10), cat.rb.velocity.y + cat.jumpHeight);
            flag = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateIntheAir : CatState
{
    private bool flag;
    public void Enter(CatMovement cat)
    {
        cat.rb.sharedMaterial = DataManager.Instance.Config.slip;
        flag = true;
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

    public void UpdateInState(CatMovement cat)
    {
        float x = cat.inputMove.x*cat.speed;
        if (flag)
        {
            cat.rb.velocity = new Vector2(cat.rb.velocity.x * (cat.speedJump / 10), cat.rb.velocity.y + cat.jumpHeight);
            flag = false;
        }
        else
        cat.rb.velocity = new Vector2(x * cat.speedJump/10, cat.rb.velocity.y);
        if (cat.IsGround())
        {
            cat.stateMachine.ChangeState(CatStateID.OnGround);
        }
        if (cat.IsClimbing())
        {
            cat.stateMachine.ChangeState(CatStateID.Climbing);
        }
    }
}

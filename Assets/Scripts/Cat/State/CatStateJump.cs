using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateJump : CatState
{
    private float x = 0;
    public void Enter(CatController cat)
    {
        cat.rb.sharedMaterial = DataManager.Instance.Config.slip;
        cat.rb.velocity = new Vector2(cat.rb.velocity.x * (cat.speedJump / 10), (cat.rb.velocity.y + cat.jumpHeight)<cat.jumpHeight?cat.jumpHeight: (cat.rb.velocity.y + cat.jumpHeight));
        
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
        if (x > 0)
        {
            cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x < 0)
        {
            cat.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        cat.rb.velocity = new Vector2(x * cat.speedJump / 10, cat.rb.velocity.y);
    }


    public void UpdateInState(CatController cat)
    {
        if (cat.rb.velocity.y < 0.1f)
        {
            cat.stateMachine.ChangeState(CatStateID.InAir);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class CatStateInAir : CatState
{
    public void Enter(CatController cat)
    {
        cat.rb.sharedMaterial = DataManager.Instance.Config.slip;
    }

    public void Exit(CatController cat)
    {
        cat.isJumping = false;
    }

    public void FixedUpdateInState(CatController cat)
    {
        float x = cat.inputMove.x * cat.speed;
        cat.rb.velocity = new Vector2(x * cat.speedJump / 10, cat.rb.velocity.y);
    }

    public CatStateID GetStateID()
    {
        return CatStateID.InAir;
    }

    public void UpdateInState(CatController cat)
    {
        if (cat.inputMove.x > 0)
        {
            cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (cat.inputMove.x < 0)
        {
            cat.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (cat.IsClimbing())
        {
            cat.stateMachine.ChangeState(CatStateID.Climbing);
        }
        if (cat.IsGround())
        {
            cat.fallParticle.Play();
            cat.stateMachine.ChangeState(CatStateID.Idle);
        }
    }
}

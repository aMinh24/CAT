using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.Windows;

public class CatStateOnGround : CatState
{
    public void Enter(CatMovement cat)
    {
        cat.rb.sharedMaterial = null;
        cat.isClimbing = false;
        Debug.Log("ground");
    }

    public void Exit(CatMovement cat)
    {
    }

    public CatStateID GetStateID()
    {
        return CatStateID.OnGround;
    }

    public void UpdateInState(CatMovement cat)
    {
        if (cat.isJumping)
        {
            cat.stateMachine.ChangeState(CatStateID.InTheAir);
        }
        float x = cat.inputMove.x * cat.speed;
        if (x != 0)
        {
            cat.animator.SetBool("isWalking", true);       
        }
        else
        {
            cat.animator.SetBool("isWalking", false);
        }
        cat.rb.velocity = new Vector2(x, cat.rb.velocity.y);
    }
}

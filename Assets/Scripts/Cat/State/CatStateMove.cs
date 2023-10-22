
using UnityEngine;

public class CatStateMove : CatState
{
    private float x = 0;
    public void Enter(CatController cat)
    {
        cat.rb.sharedMaterial = null;
        cat.dustParticle.Play();
    }

    public void Exit(CatController cat)
    {
        cat.dustParticle.Stop();
    }
    public CatStateID GetStateID()
    {
        return CatStateID.Move;
    }

    public void FixedUpdateInState(CatController cat)
    {
        
        x = cat.inputMove.x * cat.speed;
        cat.rb.velocity = new Vector2(x, cat.rb.velocity.y);
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
        if (cat.inputMove.x == 0)
        {
            cat.stateMachine.ChangeState(CatStateID.Idle);
        }
        if (cat.isJumping)
        {
            cat.stateMachine.ChangeState(CatStateID.Jump);
        }
        if (cat.rb.velocity.y < -0.1f)
        {
            cat.stateMachine.ChangeState(CatStateID.InAir);
        }
    }
}

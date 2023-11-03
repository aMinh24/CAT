
using UnityEngine;

public class CatStateMove : CatState
{
    private float x = 0;
    private float timeCoyote = 0;
    public void Enter(CatController cat)
    {
        if (DataManager.HasInstance)
        {
            timeCoyote = DataManager.Instance.Config.timeCoyote;
        }
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
            cat.rb.velocity = Vector3.zero;
            cat.stateMachine.ChangeState(CatStateID.Idle);
        }
        if (cat.isJumping)
        {
            cat.stateMachine.ChangeState(CatStateID.Jump);
        }
        if (cat.rb.velocity.y < -0.1f)
        {
            if (timeCoyote >= 0)
            {
                timeCoyote -=Time.deltaTime;
            }
            else
            {
                cat.stateMachine.ChangeState(CatStateID.InAir);
            }
            
        }
    }
}

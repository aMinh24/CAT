using System;
using UnityEngine;

public class CatStateMachine
{
    private CatMovement cat;
    private CatStateID curState;
    private CatState[] states;
    private float changeTime = 0;
    public CatStateMachine(CatMovement cat)
    {
        this.cat = cat;
        states = new CatState[Enum.GetNames(typeof(CatStateID)).Length];
    }
    
    public void RegisterState(CatState state)
    {
        int id = (int)state.GetStateID();
        states[id] = state;
    }
    public CatState GetState(CatStateID id)
    {
        return states[(int)id];
    }
    public void ChangeState(CatStateID newState)
    {
        if (changeTime > 0)
        {
            return;
        }
        CatState state = states[(int)curState];
        if (state != null)
        {
            state.Exit(cat);
        }
        curState = newState;
        state = GetState(curState);
        if (state != null)
        {
            changeTime = 0.3f;
            state.Enter(cat);
        }
    }
    // Update is called once per frame
    public void UpdateInState()
    {
        changeTime -= Time.fixedDeltaTime; 
        
        CatState state = GetState(curState);
        if (state !=null)
        {
            state.UpdateInState(cat);
        }
    }
}

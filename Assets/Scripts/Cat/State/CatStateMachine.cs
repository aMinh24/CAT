using System;
using UnityEngine;

public class CatStateMachine
{
    private CatController cat;
    private CatStateID curState;
    private CatState[] states;

    private AnimationController controller;
    public CatStateMachine(CatController cat)
    {
        this.cat = cat;
        states = new CatState[Enum.GetNames(typeof(CatStateID)).Length];
        controller = cat.controller;
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
        CatState state = states[(int)curState];
        if (state != null)
        {
            state.Exit(cat);
        }
        curState = newState;
        state = GetState(curState);
        if (state != null)
        {
            state.Enter(cat);
            controller.playAnim(curState);
        }
    }
    // Update is called once per frame
    public void UpdateInState()
    {
        Debug.Log(GetState(curState).ToString());
        CatState state = GetState(curState);
        if (state != null)
        {
            state.UpdateInState(cat);
        }
    }
    public void FixedUpdateInState()
    {
        CatState state = GetState(curState);
        if (state != null)
        {
            state.FixedUpdateInState(cat);
        }
    }
}

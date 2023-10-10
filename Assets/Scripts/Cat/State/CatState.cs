using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CatState
{
    CatStateID GetStateID();
    void Enter();
    void UpdateInState();
    void Exit();
}

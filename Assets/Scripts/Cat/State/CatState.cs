using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CatState
{
    CatStateID GetStateID();
    void Enter(CatController cat);
    void UpdateInState(CatController cat);
    void FixedUpdateInState (CatController cat);
    void Exit(CatController cat);
}

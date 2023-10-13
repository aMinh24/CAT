using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CatState
{
    CatStateID GetStateID();
    void Enter(CatMovement cat);
    void UpdateInState(CatMovement cat);
    void FixedUpdateInState (CatMovement cat);
    void Exit(CatMovement cat);
}

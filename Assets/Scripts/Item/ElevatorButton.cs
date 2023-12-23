using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : InteractItem
{
    public Elevator ele;
    public override void Interact(object data, bool f)
    {
        if (ele.isMoving)
        {
            return;
        }
        if (data is GameObject o)
        {
            ele.UseEle(o);
        }   

    }
}

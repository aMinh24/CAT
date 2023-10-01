using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorButton : InteractItem
{
    public Collider2D col;
    public Elevator ele;
    public override void Interact(object data)
    {
        if (ele.isMoving)
        {
            return;
        }
        ele.UseEle();
        this.col.enabled = true;
    }
}

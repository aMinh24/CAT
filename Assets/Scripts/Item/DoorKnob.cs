using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnob : InteractItem
{
    public BoxCollider2D door;
    public override void Interact(object data)
    {
        if (data is GameObject obj)
        {
            Interact inter = obj.GetComponent<Interact>();
           
            if (inter.rmItem(Items.Key))
            {
                door.enabled = false;
            }
        }
    }
}

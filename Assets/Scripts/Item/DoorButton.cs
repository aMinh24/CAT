using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoorButton : InteractItem
{
    public Transform door;
    private bool isOpen = false;
    public override void Interact(object data, bool f)
    {
        if (!isOpen)
        {
            door.DORotate(new Vector3(0, 0, -50), 1f);
            isOpen = true;
        }
        
    }
}

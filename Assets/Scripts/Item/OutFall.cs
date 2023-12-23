using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutFall : InteractItem
{
    public Transform lever;
    public Transform outfall;
    public override void Interact(object data, bool forceInteract = false)
    {
        lever.DORotate((lever.rotation.eulerAngles + new Vector3(0, 0, 90)), 1f);
        outfall.DOLocalMoveX(22, 0.3f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    
}

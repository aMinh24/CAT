using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractItem
{
    //private Collider2D collider;


    public override void Interact(object? data, bool forceInteract = false)
    {
        if (data is GameObject obj)
        {
            //txt.SetActive(true);
            //transform.SetParent(obj.transform);
            //collider.enabled = false;
            this.gameObject.SetActive(false);
        }
    }


}

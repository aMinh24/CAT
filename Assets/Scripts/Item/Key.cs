using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractItem
{
    private Collider2D collider;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        nameItem = Items.Key;
    }
    public override void Interact(object? data)
    {
        if (data is GameObject obj)
        {
            transform.SetParent(obj.transform);
            collider.enabled = false;
        }
    }


}

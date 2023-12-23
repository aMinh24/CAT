using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class cabinet : InteractItem
{
    public BoxCollider2D col;
    public BoxCollider2D ground;
    public GameObject init;
    public GameObject change;
    public override void Interact(object data, bool f)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE("Drawer");
        }
        transform.DOMove(new Vector3(transform.position.x, transform.position.y-0.1f, transform.position.z-0.04f),0.2f);
        col.enabled = false;
        ground.enabled = true;
        init.SetActive(false);
        change.SetActive(true);
    }
}

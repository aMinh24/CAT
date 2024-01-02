using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorFirstRoom : InteractItem
{
    public GameObject door;
    public override void Interact(object data, bool f = false)
    {
        if (!f)
        {
            AudioManager.Instance.PlaySE("CatOpenDoor");
        }
        door.SetActive(false);       
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}

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
        StartCoroutine(usingLever(forceInteract));
    }
    IEnumerator usingLever(bool f)
    {
        if (!f)
        {
            AudioManager.Instance.PlaySE("lever");
        }
        lever.DORotate((lever.rotation.eulerAngles + new Vector3(0, 0, 90)), 1f);
        yield return new WaitForSeconds(1f);
        if (!f)
        {
            AudioManager.Instance.PlaySE("OutFall");
        }
        outfall.DOLocalMoveX(22, 1f);

        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    } 
    
}

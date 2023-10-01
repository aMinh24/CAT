using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform[] floor;
    private int curFl = 0;
    public Collider2D[] doors;
    public bool isMoving = false;
    public SpriteRenderer buttonColor;
    private void Start()
    {
        transform.position = floor[curFl].position;
    }
    public void UseEle()
    {
        curFl= (curFl+1)%2;
        doors[0].enabled = true; doors[1].enabled = true;
        isMoving = true;
        buttonColor.color = Color.red;
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOMove(floor[curFl].position,3f));
        sq.OnComplete(() =>
        {
            if (curFl == 1)
            {
                doors[0].enabled = false; doors[1].enabled = true;
            }
            else
            {
                doors[0].enabled = false; doors[1].enabled = false;
            }
            isMoving = false;
            buttonColor.color = Color.green;

        });

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}

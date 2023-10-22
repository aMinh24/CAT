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
    public GameObject cat;
    private void Start()
    {
        transform.position = floor[curFl].position;
    }
    public void UseEle()
    {
        cat.transform.SetParent(this.transform);
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
            cat.transform.SetParent(null);
        });

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cat = collision.gameObject;
        }
    }
}

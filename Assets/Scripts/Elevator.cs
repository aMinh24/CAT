using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform[] floor;
    public int curFl = 0;
    public Collider2D[] doors;
    public bool isMoving = false;
    public SpriteRenderer buttonColor;

    private void Start()
    {
        transform.position = floor[curFl].position;
        this.Register(EventID.LoadData, loadGame);
    }
    public void loadGame(object? data)
    {
        if (DataManager.HasInstance)
        {
            curFl = DataManager.Instance.dataPlayerSO.curElevator;
            transform.position = floor[curFl].position;
            Debug.Log("load ele");
        }
    }
    public void UseEle(GameObject o)
    {
        o.transform.SetParent(this.transform);
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE("Elevator");
        }
        curFl= (curFl+1)%2;
        doors[0].enabled = true; doors[1].enabled = true;
        doors[2].enabled = false;
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
                doors[2].enabled = true;
            }
            isMoving = false;
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE("ElevatorBell");
            }
            buttonColor.color = Color.green;
            o.transform.SetParent(null);
        });
        if (DataManager.HasInstance)
        {
            DataManager.Instance.dataPlayerSO.curElevator = curFl;
        }

    }
}

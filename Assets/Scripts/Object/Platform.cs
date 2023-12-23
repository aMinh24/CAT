using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] point;
    private int cur;
    public float timeMove;
    private void Start()
    {
        transform.DOLocalMoveY(point[cur].localPosition.y, timeMove);
    }
    private void Update()
    {
        while (Mathf.Abs(transform.localPosition.y - point[cur].localPosition.y) < 0.1f)
        {
            cur = (cur+1)%2;
            transform.DOLocalMoveY(point[cur].localPosition.y, timeMove);
        }
    }
}

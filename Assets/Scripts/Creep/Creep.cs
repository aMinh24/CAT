using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Creep : MonoBehaviour
{
    public Transform[] point = new Transform[2];
    public float time = 15;

    private void Awake()
    {
        Movement();
    }
    private void Movement(int i = 0)
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOMove(point[i].position, time));
        sq.OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 180*((i+1)%2), 0), 0);
            Movement((i+1)%2);
        });
    }
}

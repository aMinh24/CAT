using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ArrowMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveArrow();
    }
    private void MoveArrow(int i = -1)
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOLocalMoveY(-2.965f+0.015f * i, 0.3f));
        sq.OnComplete(() =>
        {
            i = -i;
            MoveArrow(i);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CranceController : MonoBehaviour
{
    public float TimeStep;
    public Transform[] levers;
    public Transform platform;
    public Transform[] points;
    private int curpoint = 0;
    private bool direction = false; //false go down - true go up
    public void nextStepLevers()
    {
        Debug.Log("next");
        if (curpoint == 0 && direction)
        {
            direction = !direction;
        }
        if (curpoint == 2 && !direction)
        {
            direction = !direction;
        }
        if (direction)
        {
            curpoint--;
        }
        else curpoint++;
        platform.DOMove(points[curpoint].position, TimeStep);
        float angle = 0;
        switch (curpoint)
        {
            case 0:
                {
                    angle = 90f;
                    break;
                }
            case 1:
                {
                    angle = 45;
                    break;
                }
            case 2:
                {
                    angle = 0;
                    break;
                }
        }
        foreach (Transform l in levers)
        {
            l.DOLocalRotate(new Vector3(0, 0, angle), TimeStep);
        }

    }
}

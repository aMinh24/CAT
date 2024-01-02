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
    private int curpoint = 2;
    private bool direction = true; //false go up - true go down
    private bool isUsing = false;
    public IEnumerator nextStepLevers()
    {
        if (!isUsing)
        {
            isUsing = true;
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
                l.DOLocalRotate(new Vector3(0, 0, angle), 1f);
            }
            AudioManager.Instance.PlaySE("lever");
            yield return new WaitForSeconds(0.5f);
            platform.DOMove(points[curpoint].position, TimeStep);
            AudioManager.Instance.PlaySE("Crance");
            yield return new WaitForSeconds(TimeStep);
            isUsing = false;
        }
        
    }
}

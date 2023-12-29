using Cinemachine;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomShow : MonoBehaviour
{
    public SkeletonAnimation bossAnim;
    public Transform bossTransform;
    public CinemachineVirtualCamera virtualCamera;
    public Transform desPoint;
    public float timeMove;
    public AnimationReferenceAsset move;
    public AnimationReferenceAsset opendoor;
    public GameObject[] door;
    public CatController cat;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(trigger());
        }
    }
    private IEnumerator trigger()
    {
        virtualCamera.Priority = 30;
        cat.freezing = true;
        yield return new WaitForSeconds(2f);
        bossAnim.state.SetAnimation(0, move, true);
        bossTransform.DOLocalMoveX(desPoint.localPosition.x, timeMove);
        yield return new WaitForSeconds(timeMove);
        bossAnim.state.SetAnimation(0, opendoor, false);
        yield return new WaitForSeconds(2f);
        door[0].SetActive(false);
        door[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        bossTransform.gameObject.SetActive(false);
        virtualCamera.Priority = 0;
        cat.freezing = false;
    }
}

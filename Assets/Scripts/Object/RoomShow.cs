using Cinemachine;
using DG.Tweening;
using Spine;
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
        TrackEntry track = bossAnim.state.SetAnimation(0, move, true);
        bossTransform.DOLocalMoveX(desPoint.localPosition.x, timeMove);
        float time = 0;
        while (desPoint.localPosition.x - bossTransform.localPosition.x > 0.01)
        {
            time+= Time.deltaTime;
            if (time > timeMove * 2 / 3)
            {
                track.TimeScale = Mathf.Clamp01((timeMove - time)/2.2f);
            }
            yield return null;
        }

        //yield return new WaitForSeconds(timeMove-0.2f);
        //track.TimeScale = 0.6f;
        //yield return new WaitForSeconds(0.2f);
        //track.TimeScale = 1f;


        bossAnim.state.SetAnimation(0, opendoor, false);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlaySE("BossOpenDoor");
        door[0].SetActive(false);
        door[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        bossTransform.gameObject.SetActive(false);
        virtualCamera.Priority = 0;
        cat.freezing = false;
    }
}

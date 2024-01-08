using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FinalRoom : MonoBehaviour
{
    public SkeletonAnimation bossAnim;
    public Transform bossTransform;
    public CatHide cat;
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset attack;
    public Transform[] waypoints;
    public int curpoint = 0;
    public float bossSpeed;
    public bool end;
    private void Start()
    {
        end = true;
    }
    private void Update()
    {
        if (end) return;
        if (Vector2.Distance(bossTransform.position, waypoints[curpoint].position) <= 0.1f)
        {
            bossAnim.skeleton.ScaleX = -bossAnim.skeleton.ScaleX;
            curpoint = (curpoint + 1) % 2;
        }
        bossTransform.localPosition += new Vector3((curpoint == 0 ? 1 : -1) * bossSpeed * Time.deltaTime, 0, 0);
        if (!cat.isHiding)
        {
            if (cat.transform.position.x - bossTransform.position.x > 0 && bossAnim.skeleton.ScaleX == -1)
            {
                StartCoroutine(endGame());
                return;
            }
            if (cat.transform.position.x - bossTransform.position.x < 0 && bossAnim.skeleton.ScaleX == 1)
            {
                StartCoroutine(endGame());
                return;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(endGame());

    }
    IEnumerator endGame()
    {
        AudioManager.Instance.PlaySE("EndGame");
        cat.GetComponent<CatController>().freezing = true;
        end = true;
        bossAnim.skeleton.ScaleX = -1;
        bossTransform.position = cat.gameObject.transform.position - new Vector3(2.4f, 0, 0);
        Time.timeScale = 0.3f;
        yield return null;
        TrackEntry trackEntry = bossAnim.state.SetAnimation(0, attack, false);
        trackEntry.AnimationEnd = 1.15f;
        //trackEntry.TimeScale = 0.3f;
        //yield return new WaitForSpineAnimationComplete(trackEntry);
        yield return new WaitForSeconds(0.98f);
        AudioManager.Instance.PlaySE("Stab");
        yield return new WaitForSpineAnimationComplete(trackEntry);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.35f);
        AudioManager.Instance.PlaySE("EndPanel");
        yield return new WaitForSeconds(0.35f);
        UIManager.Instance.ShowScreen<EndGame>(null, true);
    }
}

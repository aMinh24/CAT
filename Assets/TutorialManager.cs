using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialManager : MonoBehaviour
{
    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<TutorialManager>();
            }
            return TutorialManager.instance;
        }
    }

    public CinemachineVirtualCamera camEle;
    private int curTutorial = 0;
    public GameObject[] tutorials = new GameObject[3];
    public GameObject cat;
    public GameObject txtJumpHere;
    public Rigidbody2D Box;
    public GameObject[] arrows;
    public BoxCollider2D button;
    public bool tuto;

    public void NextTutorial()
    {
        switch (curTutorial)
        {
            case 3:
                {
                    camEle.Priority = 20;
                    break;
                }
        }
        int n = curTutorial - 1 < 0 ? 0 : curTutorial - 1;
        if (tutorials[n] != null)
        {
            tutorials[n].SetActive(false);
        }
        if (tutorials[curTutorial] != null)
        {
            tutorials[curTutorial].SetActive(true);
        }
        this.Broadcast(EventID.Tutorial, curTutorial);
        curTutorial++;
    }
    public void NextTutorial(int n)
    {
        switch (n)
        {
            case 0:
                {
                    NextTutorial();
                    break;
                }
            case 1:
                {
                    txtJumpHere.SetActive(false);
                    arrows[0].SetActive(false);
                    arrows[1].SetActive(true);
                    Box.constraints = RigidbodyConstraints2D.None;
                    break;
                }
        }
    }
    public void CancelCase(int i)
    {
        if (tuto) return;
        switch (i)
        {
            case 1:
                {
                    if (txtJumpHere != null)
                        txtJumpHere.gameObject.SetActive(true); break;
                }
            case 3:
                {
                    camEle.Priority = 0;
                    button.enabled = true;
                    break;
                }
            case 4:
                {
                    Sequence sq = DOTween.Sequence();
                    sq.AppendInterval(10);
                    sq.OnComplete(() =>
                    {
                        tutorials[4].SetActive(false);
                    });
                    break;
                }
        }
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialManager : MonoBehaviour
{
    private static TutorialManager instance;
    public static TutorialManager Instance { get { 
            if (instance == null)
            {
                instance = FindAnyObjectByType<TutorialManager>();
            }
            return TutorialManager.instance; } }

    public CinemachineTargetGroup group;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;
    private int curTutorial = 0;
    public GameObject[] tutorials = new GameObject[3];
    public GameObject cat;
    public GameObject box;
    private void Awake()
    {
        //NextTutorial();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }
    public void NextTutorial()
    {
        switch (curTutorial)
        {
            case 3:
                {
                    virtualCamera.LookAt = group.gameObject.transform;
                    virtualCamera.Follow = group.gameObject.transform;
                    DOTween.To(() => group.m_Targets[1].weight, x => group.m_Targets[1].weight = x, 1f, 1f).SetEase(Ease.Linear);
                    //DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x,Vector3.zero, 0.2f).SetEase(Ease.Linear);
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
        Debug.Log(curTutorial);
        curTutorial++;
    }
    public void CancelCase(int i)
    {
        switch (i)
        {
            case 1:
                {
                    box.gameObject.SetActive(true); break;
                }
            case 3:
                {
                    virtualCamera.LookAt = cat.transform;
                    virtualCamera.Follow = cat.transform;
                    DOTween.To(() => group.m_Targets[1].weight, x => group.m_Targets[1].weight = x, 0f, 1f).SetEase(Ease.Linear);
                    break;
                }
        }
        
        //DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, new Vector3(0,2,0), 0.2f).SetEase(Ease.Linear);
    }
}

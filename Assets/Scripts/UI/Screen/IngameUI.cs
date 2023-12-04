using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
using EnTouch = UnityEngine.InputSystem.EnhancedTouch;

public class IngameUI : BaseScreen
{
    public RectTransform joystick;
    private Vector2 joystickPosition;
    public GameObject jumpButton;
    public Button interact;
    public Interact inter;
    private CatController cat;
    public GameObject[] tutorial = new GameObject[3];
    private int curTutorial = 0;
    public GameObject[] tip;
    public bool isShowTutorial = false;
    public OnScreenStick onScreenStick;
    public bool checkInteractButton;
    public bool lockInteractButton;                       
    public override void Hide()
    {
        base.Hide();

        EnTouch.Touch.onFingerDown -= HandleFingerDown;
        EnTouch.Touch.onFingerUp -= HandleFingerUp;
        EnhancedTouchSupport.Disable();
        this.Unregister(EventID.Tutorial, ShowTutorial);
    }

    public override void Init()
    {
        base.Init();
        checkInteractButton = false;
        lockInteractButton = false;
        inter = FindAnyObjectByType<Interact>();
        cat = inter.GetComponent<CatController>();
        inter.interact = interact;
        joystickPosition = joystick.anchoredPosition;
    }

    public override void Show(object data)
    {
        EnhancedTouchSupport.Enable();
        
        EnTouch.Touch.onFingerDown += HandleFingerDown;
        EnTouch.Touch.onFingerUp += HandleFingerUp;
        if (data is Interact i)
        {
            cat = i.gameObject.GetComponent<CatController>();
            i.interact = interact;
            inter = i;
        }
        this.Register(EventID.Tutorial, ShowTutorial);
        joystick.anchoredPosition = joystickPosition;
        TutorialManager.Instance.NextTutorial();
        base.Show(data);
        
    }
    private IEnumerator freezeScreen()
    {
        cat.freezing = true;
        lockInteractButton = true;
        yield return new WaitForSeconds(CONST.TIME_FREEZING);
        
        if (!checkInteractButton)
        {
            cat.freezing = false;
            tip[0].SetActive(true);
        }
        else tip[1].SetActive(true);
        lockInteractButton = false;
    }
    public void ShowTutorial(object data)
    {

        if (data is int i)
        {
            if (i > CONST.COUNT_TUTORIALS_UI) return;
            curTutorial = i;
            isShowTutorial = true;
            StartCoroutine(freezeScreen());
            if (i >= tutorial.Length) return;
            if (i == 1)
            {
                jumpButton.SetActive(true);
            }
            else if(i == 2)
            {
                checkInteractButton = true;
            }
            tutorial[i].SetActive(true);
            
        }
    }
    public void OnInteractButton()
    {
        if (interact != null&& !lockInteractButton)
        {
            if (checkInteractButton)
            {
                TutorialManager.Instance.CancelCase(curTutorial);
                tip[1].SetActive(false);
                isShowTutorial = false;
                if (curTutorial < tutorial.Length)
                {
                    tutorial[curTutorial].SetActive(false);
                }
                checkInteractButton =false;
            }
            inter.OnButtonInteract();
        }
        
    }
    public void OnJumpButton()
    {

        if (cat != null&&!cat.freezing)
        {    
            cat.isJumping = true;
        }
    }
    private void HandleFingerUp(Finger TouchedFinger)
    {
        if (TouchedFinger.screenPosition.x < Screen.width*2 / 3 && TouchedFinger.screenPosition.y < Screen.height * 2 / 3)
        {
            joystick.anchoredPosition = joystickPosition;
        }

    }
    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (cat.freezing) { return; }
        if (isShowTutorial && !checkInteractButton)
        {           
            TutorialManager.Instance.CancelCase(curTutorial);                
            tip[0].SetActive(false);
            isShowTutorial=false;
            if (curTutorial <tutorial.Length)
            {
                tutorial[curTutorial].SetActive(false);
            }          
        }
        if (TouchedFinger.screenPosition.x < Screen.width / 2 && TouchedFinger.screenPosition.y < Screen.height * 2 / 3)
        {
            //joystick.anchoredPosition = TouchedFinger.screenPosition;
            Vector2 point;
            RectTransform rectTransform = GetComponent<RectTransform>();
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, TouchedFinger.screenPosition,null,out point))
            {
                joystick.anchoredPosition = point;
            }
            else
            {
                Debug.Log("fail");
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;
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
    public GameObject tip;
    public bool isShowTutorial = false;
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
    public void ShowTutorial(object data)
    {

        if (data is int i)
        {
            tip.SetActive(true);
            curTutorial = i;
            isShowTutorial = true;
            if (i >= tutorial.Length) return;
            if (i == 1)
            {
                jumpButton.SetActive(true);
            }                   
            tutorial[i].SetActive(true);
            
            Debug.Log("show" + i);
        }
    }
    public void OnInteractButton()
    {
        if (interact != null)
        {
            inter.OnButtonInteract();
        }
    }
    public void OnJumpButton()
    {
        if (cat != null)
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
        if (isShowTutorial)
        {           
            TutorialManager.Instance.CancelCase(curTutorial);                
            tip.SetActive(false);
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

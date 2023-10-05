using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using EnTouch = UnityEngine.InputSystem.EnhancedTouch;

public class IngameUI : BaseScreen
{
    public RectTransform joystick;
    private Vector2 joystickPosition;
    public Button interact;
    public Interact inter;

    public override void Hide()
    {
        base.Hide();
        EnTouch.Touch.onFingerDown -= HandleFingerDown;
        EnTouch.Touch.onFingerUp -= HandleFingerUp;
        EnhancedTouchSupport.Disable();
    }

    public override void Init()
    {
        base.Init();
        joystickPosition = joystick.anchoredPosition;
    }

    public override void Show(object data)
    {
        EnhancedTouchSupport.Enable();
        
        EnTouch.Touch.onFingerDown += HandleFingerDown;
        EnTouch.Touch.onFingerUp += HandleFingerUp;
        if (data is Interact i)
        {
            i.interact = interact;
            inter = i;
        }
        base.Show(data);
        
    }
    public void OnInteractButton()
    {
        if (interact != null)
        {
            inter.OnButtonInteract();
        }
    }
    private void HandleFingerUp(Finger TouchedFinger)
    {
        if (TouchedFinger.screenPosition.x < Screen.width / 2 && TouchedFinger.screenPosition.y < Screen.height * 2 / 3)
        {
            joystick.anchoredPosition = joystickPosition;
        }

    }
    private void HandleFingerDown(Finger TouchedFinger)
    {
        
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject interact;
    public Interact inter;
    private CatController cat;
    public GameObject tutorial;
    public GameObject tip;
    public bool isShowTutorial = false;
    public bool checkInteractButton;
    public bool lockInteractButton;
    public TextMeshProUGUI[] instr;
    public Image imgIns;
    public Slider soulTime;
    private float timeCount;
    private float lifeTime;
    private bool collapse;
    private void Update()
    {
        if (collapse||cat.freezing) return;
        timeCount -= Time.deltaTime;
        soulTime.value = Mathf.Clamp01(timeCount / lifeTime);
        if (timeCount <= 0)
        {
            this.Broadcast(EventID.EndTime);
            timeCount = lifeTime;
        }
    }
    public void FullSoul(object data = null)
    {
        timeCount = lifeTime;
    }
    public void OnCollectSoul(object data = null)
    {
        timeCount += DataManager.Instance.Config.TimePerSoul;
        if(timeCount >=lifeTime) { timeCount=lifeTime; }
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    public override void Hide()
    {
        base.Hide();

        EnTouch.Touch.onFingerDown -= HandleFingerDown;
        EnTouch.Touch.onFingerUp -= HandleFingerUp;
        //EnhancedTouchSupport.Disable();
        this.Unregister(EventID.Tutorial, ShowTutorial);
    }

    public override void Init()
    {
        this.Register(EventID.CollectSoul, OnCollectSoul);
        this.Register(EventID.FullSoul, FullSoul);
        base.Init();
        checkInteractButton = false;
        lockInteractButton = false;
        inter = FindAnyObjectByType<Interact>();
        cat = inter.GetComponent<CatController>();
        inter.interact = interact;
        joystickPosition = joystick.anchoredPosition;
        lifeTime = DataManager.Instance.Config.LifeTime;
        timeCount = lifeTime;

    }

    public override void Show(object data)
    {
        //EnhancedTouchSupport.Enable();
        GameObject c = GameObject.FindGameObjectWithTag("Player");
        EnTouch.Touch.onFingerDown += HandleFingerDown;
        EnTouch.Touch.onFingerUp += HandleFingerUp;
        //if (data is Interact i)
        //{
        cat = c.gameObject.GetComponent<CatController>();
        inter = cat.GetComponent<Interact>();
        inter.interact = interact;
        //}
        this.Register(EventID.Tutorial, ShowTutorial);
        this.Register(EventID.StartUI, startUI);
        joystick.anchoredPosition = joystickPosition;
        collapse = false;
        base.Show(data);

    }
    public void startUI(object data)
    {
        if (data is Interact i)
        {
            cat = i.GetComponent<CatController>();
            i.interact = interact;
        }
    }
    public void OnPauseGameButton()
    {
        collapse = true;
        Time.timeScale = 0;
        UIManager.Instance.ShowPopup<PauseGame>(null, true);
        this.Hide();
    }

    private IEnumerator freezeScreen()
    {
        cat.freezing = true;
        collapse = true;
        lockInteractButton = true;
        yield return new WaitForSeconds(CONST.TIME_FREEZING);
        tip.SetActive(true);
        lockInteractButton = false;
    }
    public void ShowTutorial(object data)
    {
        isShowTutorial = true;
        StartCoroutine(freezeScreen());
        checkInteractButton = true;
        //tutorial.SetActive(true);
        foreach (TextMeshProUGUI t in instr)
        {
            t.enabled = true;
        }
        imgIns.enabled = true;
    }
    public void OnInteractButton()
    {
        if (interact != null && !lockInteractButton)             //check tutorial interact button
        {
            if (checkInteractButton)
            {
                tip.SetActive(false);
                isShowTutorial = false;
                //tutorial.SetActive(false);
                foreach (TextMeshProUGUI t in instr)
                {
                    t.enabled = false;
                }
                imgIns.enabled = false;
                cat.freezing = false;
                collapse = false;
            }
            inter.OnButtonInteract();
        }

    }
    public void OnJumpButton()
    {

        if (cat != null && !cat.freezing)
        {
            cat.isJumping = true;
        }
    }
    private void HandleFingerUp(Finger TouchedFinger)
    {
        if (TouchedFinger.screenPosition.x < Screen.width * 2 / 3 && TouchedFinger.screenPosition.y < Screen.height * 2 / 3)
        {
            joystick.anchoredPosition = joystickPosition;
        }

    }
    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (cat.freezing) { return; }
        if (TouchedFinger.screenPosition.x < Screen.width / 2 && TouchedFinger.screenPosition.y < Screen.height * 2 / 3)
        {
            //joystick.anchoredPosition = TouchedFinger.screenPosition;
            Vector2 point;
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, TouchedFinger.screenPosition, null, out point))
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

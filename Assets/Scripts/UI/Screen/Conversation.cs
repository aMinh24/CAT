using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnTouch = UnityEngine.InputSystem.EnhancedTouch;
public class Conversation : BaseScreen
{
    private bool next;
    public TextMeshProUGUI txt;
    public override void Hide()
    {
        base.Hide();
        EnTouch.Touch.onFingerDown -= OnFingerDown;
    }

    public override void Init()
    {
        base.Init();
        EnTouch.Touch.onFingerDown+= OnFingerDown;
    }

    public override void Show(object data)
    {
        base.Show(data);
        string start = "";
        string[] scripts = new string[0];
        if (data is string s)
        {
            switch (s)
            {
                case "cat":
                    {
                        start = DataManager.Instance.npc.startCat;
                        scripts = DataManager.Instance.npc.cat;
                        break;
                    }
            }
        }
        StartCoroutine(showScript(start, scripts));
    }
    private void OnFingerDown(Finger finger)
    {
        next = true;
    }
    private IEnumerator showScript(string start, string[] scripts)
    {
        Debug.Log(start);
        foreach(string script in scripts)
        {
            txt.SetText(start+script);
            yield return new WaitForSeconds(0.5f);
            while (!next)
            {
                yield return null;
            }
            next = false;
        }
        UIManager.Instance.ShowScreen<IngameUI>(null, true);
        yield return null;
    }
}

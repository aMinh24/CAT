using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnTouch = UnityEngine.InputSystem.EnhancedTouch;
public class Conversation : BaseScreen
{
    private bool next;
    private NPC m_Npc;
    //public TextMeshProUGUI txt;
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
        if (data is NPC npc)
        {
            m_Npc = npc;
            switch (npc.nameConversation)
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
        foreach(string script in scripts)
        {
            m_Npc.txt.SetText(script);
            yield return new WaitForSeconds(0.5f);
            while (!next)
            {
                yield return null;
            }
            next = false;
        }
        UIManager.Instance.ShowScreen<IngameUI>(null, true);
        m_Npc.boxChat.SetActive(false);
        yield return null;
    }
}

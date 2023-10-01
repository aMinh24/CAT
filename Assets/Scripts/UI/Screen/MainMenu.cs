using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseScreen
{
    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
    public void StartButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.LoadScene("Game");     
        }
    }
}

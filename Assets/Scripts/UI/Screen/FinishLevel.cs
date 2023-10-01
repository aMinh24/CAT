using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : BaseScreen
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
    public void OnReturnButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.LoadScene("Main");
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<MainMenu>(null, true);
        }
    }
}

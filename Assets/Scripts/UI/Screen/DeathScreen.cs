using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : BaseScreen
{
    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        Time.timeScale = 0;
        this.Broadcast(EventID.StopAudio);
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
    public void PlayAgain()
    {
        GameManager.Instance.LoadScene("Game");
    }
}

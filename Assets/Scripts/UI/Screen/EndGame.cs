using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : BaseScreen
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
    public void OnReplayButton()
    {
        StartCoroutine(replay());
    }
    private IEnumerator replay()
    {
        if (DataManager.HasInstance)
        {
            DataManager.Instance.dataPlayerSO.Reset();
            //DataManager.Instance.SaveGame();
            //this.Broadcast(EventID.LoadData);
        }
        UIManager.Instance.ShowScreen<StartGame>("replay", true);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : BasePopup
{
    public Slider SoundFX;
    public Slider MusicBG;
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
        float se = PlayerPrefs.GetFloat("SE_VOLUME_KEY");
        Debug.Log(se);
        SoundFX.value = se;
        MusicBG.value = PlayerPrefs.GetFloat("BGM_VOLUME_KEY");
        base.Show(data);
    }
    public void changVolumeSound(float volume)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeSEVolume(volume);
        }
    }
    public void ChangeVolumeBG(float volume)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeBGMVolume(volume);
        }
    }
    public void OnContinueButton()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.ShowScreen<IngameUI>(true);
        this.Hide();
    }
    public void DebugGame()
    {
        this.Broadcast(EventID.debug);
    }
}

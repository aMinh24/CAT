using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : BasePopup
{
    public Slider SoundFX;
    public Slider MusicBG;
    public GameObject SliderFX;
    public GameObject SliderBG;
    public override void Hide()
    {
        SliderFX.SetActive(false);
        SliderBG.SetActive(false);
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
        SliderFX.SetActive(true);
        SliderBG.SetActive(true);
        float se = PlayerPrefs.GetFloat("SE_VOLUME_KEY");
        Debug.Log(se);
        SoundFX.value = se;
        MusicBG.value = PlayerPrefs.GetFloat("BGM_VOLUME_KEY");
        
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
        UIManager.Instance.ShowScreen<IngameUI>(null,true);
        this.Hide();
    }
    public void DebugGame()
    {
        this.Hide();
        Time.timeScale = 1.0f;
        UIManager.Instance.ShowScreen<EndGame>(null, true);
    }
    public void SaveGame()
    {
        DataManager.Instance.SaveGame();
    }
}

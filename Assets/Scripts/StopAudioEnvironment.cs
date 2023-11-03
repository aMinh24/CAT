using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAudioEnvironment : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    private void OnEnable()
    {
        AudioManager.Instance.audioEnvironments.Add(this);
    }
    private void OnDisable()
    {
        AudioManager.Instance.audioEnvironments.Remove(this);
    }
    public void StopAudio()
    {
        audioSource.enabled = false;
    }
}

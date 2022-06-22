using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Tooltip("Theme's music")] private StudioEventEmitter m_themeMusic;
    [SerializeField, Tooltip("UI Button Press sound")] private StudioEventEmitter m_uiButtonPress;
    [SerializeField, Tooltip("UI Button Release sound")] private StudioEventEmitter m_uiButtonRelease;
    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if(!m_themeMusic.IsPlaying()) m_themeMusic.Play();
    }
    public void PlayUIButtonPress()
    {
        if(m_uiButtonPress.IsPlaying()) m_uiButtonPress.Stop();
        m_uiButtonPress.Play();
    }
    public void PlayUIButtonRelease()
    {
        if(m_uiButtonRelease.IsPlaying()) m_uiButtonRelease.Stop();
        m_uiButtonRelease.Play();
    }

    protected override string GetSingletonName()
    {
        return "SoundManager";
    }
}

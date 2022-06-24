using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : Singleton<SoundManager>
{
    [Header("MUSIC")]
    [SerializeField, Tooltip("Theme's music")] private StudioEventEmitter m_themeMusic;
    [Header("UI")]
    [SerializeField, Tooltip("UI Button Press sound")] private StudioEventEmitter m_uiButtonPress;
    [SerializeField, Tooltip("UI Button Release sound")] private StudioEventEmitter m_uiButtonRelease;
    [SerializeField, Tooltip("UI Button Select sound")] private StudioEventEmitter m_uiButtonSelect;
    [Header("GAMEPLAY")]
    [SerializeField, Tooltip("HitWall sound")] private StudioEventEmitter m_hitWall;
    [SerializeField, Tooltip("CatchBall sound")] private StudioEventEmitter m_catchBall;
    [SerializeField, Tooltip("ChargeBall sound")] private StudioEventEmitter m_chargeBall;
    [SerializeField, Tooltip("PropulseBall sound")] private StudioEventEmitter m_propulseBall;
    [SerializeField, Tooltip("GoalExplosion sound")] private StudioEventEmitter m_goalExplosion;
    private void Start()
    {
        PlayMusic();
    }
    
    // ---------------------------------------- MUSIC ----------------------------------------
    public void PlayMusic()
    {
        if(!m_themeMusic.IsPlaying()) m_themeMusic.Play();
    }
    
    // ---------------------------------------- UI ----------------------------------------
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
    public void PlayUIButtonSelect()
    {
        if(m_uiButtonSelect.IsPlaying()) m_uiButtonSelect.Stop();
        m_uiButtonSelect.Play();
    }

    // ---------------------------------------- GAMEPLAY ----------------------------------------
    public void PlayHitWall()
    {
        if(m_hitWall.IsPlaying()) return;
        m_hitWall.Play();
    }
    public void PlayCatchBall()
    {
        if(m_catchBall.IsPlaying()) m_catchBall.Stop();
        m_catchBall.Play();
    }
    public void PlayChargeBall()
    {
        if(m_chargeBall.IsPlaying()) m_chargeBall.Stop();
        m_chargeBall.Play();
    }
    public void StopChargeBall()
    {
        if(m_chargeBall.IsPlaying()) m_chargeBall.Stop();
    }
    public void PlayPropulseBall()
    {
        if(m_propulseBall.IsPlaying()) m_propulseBall.Stop();
        m_propulseBall.Play();
    }
    public void PlayGoalExplosion()
    {
        if(m_goalExplosion.IsPlaying()) m_goalExplosion.Stop();
        m_goalExplosion.Play();
    }
    protected override string GetSingletonName()
    {
        return "SoundManager";
    }
}

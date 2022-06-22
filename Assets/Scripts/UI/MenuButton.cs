using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField, Tooltip("Button Animator")] private Animator m_buttonAnimator;
    private int m_selectedAnimation = Animator.StringToHash("Selected");
    private int m_unselectedAnimation = Animator.StringToHash("Unselected");
    private int m_pressedAnimation = Animator.StringToHash("Pressed");
    private int m_releasedAnimation = Animator.StringToHash("Released");

    public void FirstSelected()
    {
        ResetAllTrigger();
        m_buttonAnimator.SetTrigger(m_selectedAnimation);
    }
    public void Selected()
    {
        if (!SceneManager.Instance.CanPlay) return;
        ResetAllTrigger();
        SoundManager.Instance.PlayUIButtonSelect();
        m_buttonAnimator.SetTrigger(m_selectedAnimation);
    }
    public void Unselected()
    {
        if (!SceneManager.Instance.CanPlay) return;
        ResetAllTrigger();
        m_buttonAnimator.SetTrigger(m_unselectedAnimation);
        Interact();
    }
    public void Pressed()
    {
        if (!SceneManager.Instance.CanPlay) return;
        ResetAllTrigger();
        SoundManager.Instance.PlayUIButtonPress();
        m_buttonAnimator.SetTrigger(m_pressedAnimation);
    }
    public void Released()
    {
        if (!SceneManager.Instance.CanPlay) return;
        ResetAllTrigger();
        SoundManager.Instance.PlayUIButtonRelease();
        m_buttonAnimator.SetTrigger(m_releasedAnimation);
    }

    private void Interact()
    {
        if (!SceneManager.Instance.CanPlay) return;
        Debug.Log(gameObject.name);
    }
    private void ResetAllTrigger()
    {
        m_buttonAnimator.ResetTrigger(m_releasedAnimation);
        m_buttonAnimator.ResetTrigger(m_selectedAnimation);
        m_buttonAnimator.ResetTrigger(m_unselectedAnimation);
        m_buttonAnimator.ResetTrigger(m_pressedAnimation);
    }
}

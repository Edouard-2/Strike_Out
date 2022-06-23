using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelecterController : MonoBehaviour
{
    public MasterPlayerController m_masterPlayerController;
    public PlayerManager m_playerManager;
    
    private PlayerInput m_playerInput;
    
    public enum States
    {
        NULL,
        SPONSOR,
        GAMEPLAY
    }

    public States m_state;

    public bool m_isReady;
    
    private void OnEnable()
    {
        m_state = States.SPONSOR;
        
        m_playerInput = GetComponent<PlayerInput>();
        
        m_playerInput.currentActionMap["Select"].started += Select_Started;
        m_playerInput.currentActionMap["Select"].canceled += Select_Canceled;
    }
    
    private void OnDisable()
    {
        if (m_playerInput == null || m_playerInput.currentActionMap == null) return;
        m_playerInput.currentActionMap["Select"].started -= Select_Started;
        m_playerInput.currentActionMap["Select"].canceled -= Select_Canceled;
    }

    private void Select_Started(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        
        switch (m_state)
        {
            case States.NULL:
                break;
            case States.SPONSOR:
                break;
            case States.GAMEPLAY:
                GameManager.Instance.StartReadyButton(m_masterPlayerController.m_id);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Select_Canceled(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        
        switch (m_state)
        {
            case States.NULL:
                break;
            case States.SPONSOR:
                if (m_isReady) break;
                m_isReady = true;
                SponsorManager.Instance.Ready(m_masterPlayerController.m_id);
                break;
            case States.GAMEPLAY:
                m_masterPlayerController.ActiveGameplayPlayer();
                GameManager.Instance.OnPlayerJoin(m_masterPlayerController);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

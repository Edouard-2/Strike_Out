using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplashScreen : MonoBehaviour
{
    private PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    public void OnSelect()
    {
        // SceneManager qui nous envoi vers le menu
        Debug.Log("ndsk");
    }
}

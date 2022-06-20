using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplashScreen : MonoBehaviour
{
    private PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerInput.currentActionMap["Select"].started += context => Interact();
    }

    public void Interact()
    {
        // SceneManager qui nous envoi vers le menu
        Debug.Log("ndsk");
        SceneManager.Instance.GoToScene(1);
    }
}

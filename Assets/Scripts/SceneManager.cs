using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    private string m_nameCurrentScene = null;

    private void Awake()
    {
        GoToScene("SplashScreen");
    }

    public void GoToScene(string value)
    {
        if(m_nameCurrentScene != null) UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_nameCurrentScene);
        // Lance la scene de load
        m_nameCurrentScene = value;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
    }
    
    protected override string GetSingletonName()
    {
        return "SceneManager";
    }
}

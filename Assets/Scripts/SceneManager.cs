using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    private string m_nameCurrentScene = null;
    private int m_idCurrentScene = 0;

    private void Awake()
    {
        GoToScene(1);
    }

    public void GoToScene(string value)
    {
        if(m_nameCurrentScene != null) UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_nameCurrentScene);
        // Lance la scene de load
        m_nameCurrentScene = value;
        m_idCurrentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(value).buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
    }
    public void GoToScene(int value)
    {
        if(m_nameCurrentScene != null) UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_nameCurrentScene);
        // Lance la scene de load
        m_nameCurrentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(value).name;
        m_idCurrentScene = value;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
    }
    
    protected override string GetSingletonName()
    {
        return "SceneManager";
    }
}

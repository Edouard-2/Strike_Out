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

    // Animator de la Transition
    [SerializeField, Tooltip("BlackTransition in the SceneManager's Canvas")] private Animator m_blackTransitionAnimator = null;
    private int m_openAnimation = Animator.StringToHash("Open");
    private int m_closeAnimation = Animator.StringToHash("Close");
    
    // Animator du Loader
    [SerializeField, Tooltip("Loader in the SceneManager's Canvas")] private Animator m_loaderAnimator = null;
    private int m_popAnimation = Animator.StringToHash("Pop");
    private int m_depopAnimation = Animator.StringToHash("Depop");

    private bool m_onChange = false;

    private void Awake()
    {
        m_nameCurrentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1).name;
        m_idCurrentScene = 1;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
    
    /// <summary>
    /// Change game scene to an other scene from a build index (LoadSceneMode.Additive).
    /// </summary>
    /// <param name="buildIndex">Build index as shown in the Build Settings window.</param>
    public void GoToScene(int buildIndex)
    {
        if (m_onChange) return;
        m_onChange = true;
        if (m_nameCurrentScene != null)
        {
            Debug.Log(m_nameCurrentScene);
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_nameCurrentScene);
        }

        StartCoroutine(UnloadAsync(buildIndex));
    }

    private void StartTransition()
    {
        m_blackTransitionAnimator.SetTrigger(m_closeAnimation);
    }
    private void EndTransition()
    {
        m_blackTransitionAnimator.SetTrigger(m_openAnimation);
        
        m_onChange = false;
    }
    
    IEnumerator UnloadAsync(int buildIndex)
    {
        // On fait la transition
        StartTransition();

        // On décharge la scène
        if (m_idCurrentScene > 0)
        {
            float seconds = 1f;
            foreach (var animatorClipInfo in m_blackTransitionAnimator.GetCurrentAnimatorClipInfo(0))
            {
                seconds = animatorClipInfo.clip.length;
            }
            yield return new WaitForSeconds(seconds);
            AsyncOperation asynOp = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_idCurrentScene);
            m_loaderAnimator.SetTrigger(m_popAnimation);
            // On attend que le déchargement soit fait
            while (!asynOp.isDone)
            {
                yield return null;
            }
        }

        m_nameCurrentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(buildIndex).name;
        m_idCurrentScene = buildIndex;
        // On charge la scène
        StartCoroutine(LoadAsync(buildIndex));
    }
    
    
    IEnumerator LoadAsync(int buildIndex)
    {
        // On charge la scène
        AsyncOperation asynOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);

        // On attend que le chargement soit fait
        while (!asynOp.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        m_loaderAnimator.SetTrigger(m_depopAnimation);
        // On fait la transition
        EndTransition();
    }
    protected override string GetSingletonName()
    {
        return "SceneManager";
    }
}

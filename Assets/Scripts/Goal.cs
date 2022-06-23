using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Goal : MonoBehaviour
{
    
    //--------------------------Color----------------------------//
    [Header("Color")]
    [SerializeField, Tooltip("3 couleurs pour le changement d'état")] private List<Color> m_listColor = new List<Color>();
    
    //--------------------------Component----------------------------//
    [Header("Component")]
    [SerializeField, Tooltip("Sprite Renderer")] private SpriteRenderer m_spreiteRenderer;
    
    //--------------------------Public Hide----------------------------//
    [HideInInspector] public PlayerManager m_playerManager;
    
    //--------------------------Private----------------------------//
    private int m_index;

    private void Awake()
    {
        AddScore();
    }

    private void AddScore()
    {
        m_spreiteRenderer.color = m_listColor[m_index];
        m_index++;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (m_index >= m_listColor.Count)
        {
            GameManager.Instance.Win(m_playerManager);
            return;
        }
        AddScore();
    }
}

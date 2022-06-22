using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //--------------------------Components----------------------------//
    [SerializeField, Tooltip("Le rigidbody de la ball")] private Rigidbody2D m_rb;
    [SerializeField, Tooltip("Le Circle collider 2D de la ball")] private CircleCollider2D m_collider;
    
    //--------------------------Layer----------------------------//
    [SerializeField, Tooltip("Les layers qui ne son pas le joueur")] private LayerMask m_layerPlayer;
    
    //--------------------------Camera----------------------------//
    [SerializeField, Tooltip("La durée du shake de camera")] public float m_durationShake = 0.1f;
    
    //--------------------------OTHER SCRIPT----------------------------//
    private BallController m_ballController;

    private void Awake()
    {
        // Create Component
        m_ballController = gameObject.AddComponent<BallController>();

        // Init variables component
        m_ballController.m_rb = m_rb;
        m_ballController.m_durationShake = m_durationShake;
        m_ballController.m_collider = m_collider;
    }
}
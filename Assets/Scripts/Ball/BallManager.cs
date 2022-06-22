using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //--------------------------Components----------------------------//
    [SerializeField, Tooltip("Le rigidbody de la ball")] private Rigidbody2D m_rb;
    [SerializeField, Tooltip("Le Circle collider 2D de la ball")] private CircleCollider2D m_collider;
    
    //--------------------------Layer----------------------------//
    [SerializeField, Tooltip("Les layers qui ne son pas le joueur")] private LayerMask m_layerPlayer;
    
    //--------------------------OTHER SCRIPT----------------------------//
    private BallController m_ballController;

    private void Awake()
    {
        // Create Component
        m_ballController = gameObject.AddComponent<BallController>();

        // Init variables component
        m_ballController.m_rb = m_rb;
        m_ballController.m_collider = m_collider;
    }

    private void Start()
    {
        m_ballController.HitDirection(transform.up);
    }
}
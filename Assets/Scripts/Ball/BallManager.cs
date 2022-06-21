using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //--------------------------Components----------------------------//
    [SerializeField, Tooltip("Le rigidbody de la ball")] private Rigidbody2D m_rb;
    
    //--------------------------Layer----------------------------//
    [SerializeField, Tooltip("Les layers qui ne son pas le joueur")] private LayerMask m_layerNoPlayer;
    
    //--------------------------OTHER SCRIPT----------------------------//
    private BallController m_ballController;

    private void Awake()
    {
        // Create Component
        m_ballController = gameObject.AddComponent<BallController>();

        // Init variables component
        m_ballController.m_rb = m_rb;
    }

    private void Start()
    {
        m_ballController.HitDirection(transform.up);
    }

}
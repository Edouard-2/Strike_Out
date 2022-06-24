using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //--------------------------Components----------------------------//
    [SerializeField, Tooltip("Le rigidbody de la ball")] private Rigidbody2D m_rb;
    [SerializeField, Tooltip("Le Circle collider 2D de la ball")] private CircleCollider2D m_collider;
    
    //--------------------------Layer----------------------------//
    [SerializeField, Tooltip("Les layers permette de toucher les sponsors")] private LayerMask m_layerSponsor;
    
    //--------------------------Camera----------------------------//
    [SerializeField, Tooltip("La durée du shake de camera")] public List<GameObject> m_omega =new List<GameObject>();
    //--------------------------Camera----------------------------//
    [SerializeField, Tooltip("La durée du shake de camera")] public float m_durationShake = 0.1f;
    
    //--------------------------OTHER SCRIPT----------------------------//
    private BallController m_ballController;
    private List<GameObject> m_listOmega = new List<GameObject>();

    private void Awake()
    {
        // Create Component
        m_ballController = gameObject.AddComponent<BallController>();

        // Init variables component
        m_ballController.m_rb = m_rb;
        m_ballController.m_layerSponsor = m_layerSponsor;
        m_ballController.m_durationShake = m_durationShake;
        m_ballController.m_collider = m_collider;
    }

    public void ActiveOmega(int id)
    {
        GameObject go = Instantiate(m_omega[id], Vector2.zero, Quaternion.identity);
        m_listOmega.Add(go);
    }
}
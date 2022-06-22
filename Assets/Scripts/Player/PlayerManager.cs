using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    //--------------------------Components----------------------------//*
    [Header("Components")] 
    [FormerlySerializedAs("m_controls")] [SerializeField, Tooltip("Player Input du joueur")] private PlayerInput m_playerInput;
    [SerializeField, Tooltip("Circle collider 2D du joueur")] private CircleCollider2D m_collider;
    
    //--------------------------Layer Mask----------------------------//*
    [Header("Layer")] 
    [SerializeField, Tooltip("Layer de la ball")] private LayerMask m_layerBall;
    
    //--------------------------Controller Variables----------------------------//
    [Header("Controller")] 
    [SerializeField, Tooltip("Vitesse des mouvements du joueur")] private float m_speedMovement = 10;
    
    //--------------------------Controller Variables----------------------------//
    [Header("Interaction")] 
    [SerializeField, Tooltip("Vitesse des mouvements du joueur")] private float m_timeHoldingMax = 1f;
    
    //--------------------------OTHER SCRIPT----------------------------//
    private PlayerController m_playerController;
    private PlayerInteraction m_playerInteraction;
    private PlayerSkillsData m_playerSkillsData;



    private void Awake()
    {
        // Create Component
        m_playerController = gameObject.AddComponent<PlayerController>();
        m_playerInteraction = gameObject.AddComponent<PlayerInteraction>();
        m_playerSkillsData = gameObject.AddComponent<PlayerSkillsData>();
        
        // Init variables component
        m_playerInput = GetComponent<PlayerInput>();
        
        //Controller
        m_playerController.m_playerInteraction = m_playerInteraction;
        m_playerController.m_speedMovement = m_speedMovement;
        m_playerController.m_controls = m_playerInput;
        m_playerController.InitInputAction();
        
        //Interact
        m_playerInteraction.m_collider = m_collider;
        m_playerInteraction.m_timeHoldingMax = m_timeHoldingMax;
        m_playerInteraction.m_layerBall = m_layerBall;
        m_playerInteraction.m_controls = m_playerInput;
        m_playerInteraction.InitInputAction();
    }
    

    private void Update()
    {
        m_playerController.DoUpdate();
    }
}

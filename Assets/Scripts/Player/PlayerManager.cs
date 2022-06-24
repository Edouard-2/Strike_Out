using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    //--------------------------Components----------------------------//
    [Header("Components")] 
    [FormerlySerializedAs("m_controls")] [SerializeField, Tooltip("Player Input du joueur")] private PlayerInput m_playerInput;
    [SerializeField, Tooltip("Circle collider 2D du joueur")] private BoxCollider2D m_collider;
    [SerializeField, Tooltip("Sprite Renderer du feedback grab")] private SpriteRenderer m_spriteRenderer;
    
    //--------------------------Layer Mask----------------------------//
    [Header("Layer")] 
    [SerializeField, Tooltip("Layer de la ball")] private LayerMask m_layerBall;
    
    //--------------------------Layer Mask----------------------------//
    [HideInInspector] public Goal m_goal;
    
    //--------------------------Controller Variables----------------------------//
    [Header("Controller")] 
    [SerializeField, Tooltip("Vitesse des mouvements du joueur")] private float m_speedMovement = 10;
    
    //--------------------------Interaction Variables----------------------------//
    [Header("Interaction")] 
    [SerializeField, Tooltip("Vitesse des mouvements du joueur")] private float m_timeHoldingMax = 1f;
    
    //--------------------------OTHER SCRIPT----------------------------//
    [HideInInspector] public PlayerController m_playerController;
    [HideInInspector] public PlayerInteraction m_playerInteraction;
    [HideInInspector] public MasterPlayerController m_masterPlayerController;

    private void Awake()
    {
        // Create Component
        m_playerController = gameObject.AddComponent<PlayerController>();
        m_playerInteraction = gameObject.AddComponent<PlayerInteraction>();
        
        // Init variables component
        m_playerInput = GetComponent<PlayerInput>();

        //Controller
        InitControllerScript();
        
        //Interact
        InitInteractionScript();
    }

    private void InitControllerScript()
    {
        m_playerController.m_playerInteraction = m_playerInteraction;
        m_playerController.m_speedMovement = m_speedMovement;
        m_playerController.m_controls = m_playerInput;
        m_playerController.InitInputAction();
    }

    private void InitInteractionScript()
    {
        m_playerInteraction.m_spriteRenderer = m_spriteRenderer;
        m_playerInteraction.m_collider = m_collider;
        m_playerInteraction.m_timeHoldingMax = m_timeHoldingMax;
        m_playerInteraction.m_layerBall = m_layerBall;
        m_playerInteraction.m_controls = m_playerInput;
        m_playerInteraction.InitInputAction();
    }
    
    public void InitGoalScript()
    {
        m_goal.m_playerManager = this;
    }
    
    private void Update()
    {
        m_playerController.DoUpdate();
    }

    public void ResetPlayerVariables()
    {
        if (GameManager.Instance.m_ballInGame != null)
        {
            GameManager.Instance.m_ballInGame.GetComponent<BallManager>().m_listSmol.ForEach(DestroyImmediate);
            GameManager.Instance.m_ballInGame.GetComponent<BallManager>().m_listSmol = new List<GameObject>();
        }
        m_playerInteraction.transform.localScale = Vector2.one * 0.5f;
        m_playerInteraction.m_spriteRenderer = m_spriteRenderer;
        m_playerInteraction.m_timeHoldingMax = m_timeHoldingMax;
        m_playerInteraction.m_ghostBall = 0;
        m_playerController.m_speedMovement = m_speedMovement;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    //------------------------------------------------------//
    
    //--------------------------Input System----------------------------//*
    [SerializeField, Tooltip("Input Action Asset pour le movement des joueurs")] private InputActionAsset m_movementActions;
    
    //--------------------------Controller Variables----------------------------//
    [Header("Controller")] 
    [SerializeField, Tooltip("Vitesse des mouvements du joueur")] private float m_speedMovement = 10;
    
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
        m_playerController.m_speedMovement = m_speedMovement;
        
        m_playerController.m_movementActions = m_movementActions.FindAction("Movement");
        m_playerController.m_interactAction = m_movementActions.FindAction("Interact");
    }

    private void Update()
    {
        m_playerController.DoUpdate();
        //m_playerInteraction.DoUpdate();
        //m_playerSkillsData.DoUpdate();
    }
}

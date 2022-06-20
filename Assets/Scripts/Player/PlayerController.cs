using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //------------------------Public------------------------------//
    
    //--------------------------Controller----------------------------//
    public float m_speedMovement = 10;
    public Vector2 m_move;
    
    //-------------------------Input System-----------------------------//
    private PlayerControls m_controls;
    
    //---------------------------Private---------------------------//
    private Rigidbody2D m_rb;

    
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_controls = new PlayerControls();

        m_controls.Player.Movement.performed += ctxt => m_move = ctxt.ReadValue<Vector2>();
        m_controls.Player.Movement.canceled += ctxt => m_move = Vector2.zero;
    }

    private void OnEnable()
    {
        m_controls.Player.Enable();
    }

    private void OnDisable()
    {
        m_controls.Player.Disable();
    }

    public void DoUpdate()
    {
        Debug.Log(m_move);
        m_rb.MovePosition( m_rb.position + m_move * m_speedMovement * Time.deltaTime);
    }
}
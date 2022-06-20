using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //--------------------------Controller----------------------------//
    public float m_speedMovement = 10;
    
    //-------------------------Input System-----------------------------//
    public PlayerInput m_controls;
    
    //---------------------------Private---------------------------//
    private Rigidbody2D m_rb;

    
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    public void DoUpdate()
    {
        m_rb.MovePosition( m_rb.position + m_controls.currentActionMap["Movement"].ReadValue<Vector2>() * m_speedMovement * Time.deltaTime);
        m_rb.MoveRotation( quaternion.Euler(new Vector3(0,0,Mathf.Atan2(-m_controls.currentActionMap["Look"].ReadValue<Vector2>().x, m_controls.currentActionMap["Look"].ReadValue<Vector2>().y) )));
        
        //transform.rotation = Quaternion.Euler(new Vector3(0,0,m_controls.currentActionMap["Look"].ReadValue<Vector2>().y));
        //Debug.Log(m_controls.currentActionMap["Look"].ReadValue<Vector2>());
    }
}
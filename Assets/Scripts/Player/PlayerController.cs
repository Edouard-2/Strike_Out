using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //------------------------Public------------------------------//
    
    //--------------------------Controller----------------------------//
    public float m_speedMovement = 10;
    
    //-------------------------Input System-----------------------------//
    public InputAction m_movementActions;
    public InputAction m_interactAction;
    
    //---------------------------Private---------------------------//
    private Rigidbody2D m_rb;
    
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    public void DoUpdate()
    {
        Debug.Log(m_movementActions.ReadValue<Vector2>());
        m_rb.MovePosition( m_rb.position + m_movementActions.ReadValue<Vector2>() * m_speedMovement * Time.deltaTime);
    }
}
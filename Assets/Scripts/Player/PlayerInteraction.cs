using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //---------------------------Player Input---------------------------//
    public PlayerInput m_controls;

    //---------------------------Layer---------------------------//
    public LayerMask m_layerBall;

    //---------------------------Private---------------------------//
    private BallController m_currentBall;

    private float m_timePressed;
    
    private float m_timeHolding;
    private float m_timeHoldingMax = 0.5f;

    public void InitInputAction()
    {
        m_controls.currentActionMap["Interact"].started += context => StartPropulse();
        m_controls.currentActionMap["Interact"].canceled += context => PropulseBall();
    }

    /// <summary>
    /// Check if the player can propulse a ball
    /// </summary>
    private void StartPropulse()
    {
        if (m_currentBall == null) return;

        m_timePressed = Time.time;

        m_currentBall.transform.SetParent(transform);
        m_currentBall.StopBall();
    }

    /// <summary>
    /// Propulse the ball after released input
    /// </summary>
    private void PropulseBall()
    {
        if (m_currentBall == null) return;

        m_currentBall.transform.SetParent(null);
        
        m_timeHolding = Time.time - m_timePressed;
        Debug.Log(m_timeHolding);
        if (m_timeHolding > m_timeHoldingMax) m_timeHolding = m_timeHoldingMax;

        m_currentBall.Propulse(transform.up, m_timeHolding);
    }

    private bool VerifyIfBall(GameObject go)
    {
        Debug.Log("Verifier si le game manager est en play");

        if (m_currentBall != null && go == m_currentBall.gameObject) return false;

        return (m_layerBall.value & (1 << go.layer)) > 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("verif si ball"+!VerifyIfBall(col.gameObject));
        if (!VerifyIfBall(col.gameObject)) return;

        m_currentBall = col.gameObject.GetComponent<BallController>();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        m_currentBall = null;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!VerifyIfBall(col.gameObject)) return;

        m_currentBall = null;
    }
}
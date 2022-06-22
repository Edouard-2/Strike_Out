using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //---------------------------Player Input---------------------------//
    public PlayerInput m_controls;

    //---------------------------Layer---------------------------//
    public LayerMask m_layerBall;
    
    //---------------------------Component---------------------------//
    public CircleCollider2D m_collider;

    //---------------------------Private---------------------------//
    private BallController m_currentBall;

    private Coroutine m_coroutineHold;
    
    private float m_timePressed;
    
    private float m_time;
    
    private float m_timeHolding;
    public float m_timeHoldingMax = 0.5f;
    private bool m_hasCatched;

    public void InitInputAction()
    {
        m_controls.currentActionMap["Interact"].started += context => StartPropulse();
        m_controls.currentActionMap["Interact"].canceled += context => PropulseBall();
    }

    public bool HasCatched()
    {
        return m_hasCatched;
    }

    /// <summary>
    /// Check if the player can propulse a ball
    /// </summary>
    private void StartPropulse()
    {
        if (m_currentBall == null) return;
        
        m_hasCatched = true;
        Debug.Log(m_currentBall);
        m_timePressed = Time.time;

        //m_collider.enabled = false;
        
        m_currentBall.transform.SetParent(transform);
        m_currentBall.transform.position = transform.position + transform.up * 2;
        m_currentBall.StopBall();

        m_time = 0;
        //if(m_coroutineHold == null) m_coroutineHold = StartCoroutine(PropulseIfHoldIsToLong());
    }

    IEnumerator PropulseIfHoldIsToLong()
    {
        while (m_time < m_timeHoldingMax)
        {
            m_time += Time.deltaTime;
            yield return null;
        }
        
        //Reset Value
        m_time = 0;
        m_coroutineHold = null;
        Debug.Log("Propulse If Hold IsToLong");
        PropulseBall();
    }
    
    /// <summary>
    /// Propulse the ball after released input
    /// </summary>
    private void PropulseBall()
    {
        if (!m_hasCatched) return;
        m_hasCatched = false;
        if(m_coroutineHold != null) StopCoroutine(m_coroutineHold);
        
        Debug.Log(m_currentBall);
        m_currentBall.transform.SetParent(null);
        
        m_timeHolding = Time.time - m_timePressed + 1;
        Debug.Log(m_timeHolding);
        if (m_timeHolding > m_timeHoldingMax) m_timeHolding = m_timeHoldingMax;

        m_currentBall.Propulse(transform.up, m_timeHolding);
        
        m_currentBall = null;
    }

    /// <summary>
    /// Verify if the Game object is in the layer Ball
    /// </summary>
    /// <param name="go"> The gameObject verify </param>
    /// <returns></returns>
    private bool VerifyIfBall(GameObject go)
    {
        Debug.Log("Verifier si le game manager est en play");

        if (m_currentBall != null && go == m_currentBall.gameObject) return false;

        return (m_layerBall.value & (1 << go.layer)) > 0;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("verif si ball"+!VerifyIfBall(col.gameObject));
        if (!VerifyIfBall(col.gameObject)) return;

        m_currentBall = col.gameObject.GetComponent<BallController>();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        m_currentBall = null;
    }
}
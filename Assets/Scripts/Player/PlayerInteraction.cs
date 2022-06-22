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
    public BoxCollider2D m_collider;
    public SpriteRenderer m_spriteRenderer;

    //---------------------------Private---------------------------//
    private BallController m_currentBall;

    private Coroutine m_coroutineHold;
    
    private float m_timePressed;
    
    private float m_timeHolding;
    public float m_timeHoldingMax = 0.5f;
    private bool m_hasCatched;
    private Coroutine m_coroutineTrans;

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
        
        //m_collider.gameObject.SetActive(true);
        
        m_hasCatched = true;
        m_currentBall.m_isCatched = true;
        
        m_currentBall.transform.SetParent(transform);
        m_currentBall.StopBall();

        //Transition de la ball vers le point de lancement
        if(m_coroutineTrans == null) m_coroutineTrans = StartCoroutine(TransBallInFront());
        
        //Faire lacher la balle si on la garde trop longtemps
        if(m_coroutineHold == null) m_coroutineHold = StartCoroutine(PropulseIfHoldIsToLong());
    }

    IEnumerator TransBallInFront()
    {
        float time = 0;
        while (time < 0.2f)
        {
            time += Time.deltaTime;
            m_currentBall.transform.position = Vector2.Lerp(m_currentBall.transform.position, transform.position + transform.up * 2, time * 5);
            yield return null;
        }
        
        Debug.Log("Trans");
    }

    IEnumerator PropulseIfHoldIsToLong()
    {
        float time = 0;
        while (time < m_timeHoldingMax)
        {
            time += Time.deltaTime;
            m_spriteRenderer.size += Vector2.one * Time.deltaTime / m_timeHoldingMax;
            yield return null;
        }
        
        m_spriteRenderer.size = Vector2.zero;
        
        Debug.Log("Propulse If Hold IsToLong");
        
        if (m_hasCatched) PropulseBall();
    }
    
    /// <summary>
    /// Propulse the ball after released input
    /// </summary>
    private void PropulseBall()
    {
        if (!m_hasCatched) return;
        m_hasCatched = false;
        
        m_spriteRenderer.size = Vector2.zero;
        
        if(m_coroutineHold != null) StopCoroutine(m_coroutineHold);
        if(m_coroutineTrans != null) StopCoroutine(m_coroutineTrans);
        
        //m_collider.gameObject.SetActive(false);
        
        m_coroutineHold = null;
        m_coroutineTrans = null;
        
        m_currentBall.m_isCatched = false;
        
        m_currentBall.transform.SetParent(null);
        m_currentBall.Propulse(transform.up);
        
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

        //Si c'est un autre balle qui arrive
        if (m_currentBall != null && go == m_currentBall.gameObject) return false;

        //Si c'est un obj dans le layer balle
        return (m_layerBall.value & (1 << go.layer)) > 0;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("verif si ball"+!VerifyIfBall(col.gameObject));
        if (!VerifyIfBall(col.gameObject)) return;

        m_currentBall = col.gameObject.GetComponent<BallController>().Able();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (m_hasCatched) return;
        m_currentBall = null;
    }
}
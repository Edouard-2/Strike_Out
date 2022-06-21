using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    
    //---------------------------Components---------------------------//
    public Rigidbody2D m_rb;
    
    //---------------------------Private---------------------------//
    private float m_velocityMultiplier = 5f;
    
    
    /// <summary>
    /// Change the direction of the ball when it's hit
    /// </summary>
    /// <param name="dir"> The direction pointing by the hit object </param>
    public void HitDirection(Vector3 dir)
    {
        //Changer la direction de la balle
        m_rb.velocity = dir;
    }

    /// <summary>
    /// Stop the ball when a player will shoot it
    /// </summary>
    public void StopBall()
    {
        transform.localScale = Vector2.one * 1.2f;
        m_rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Propulse the ball when the player release the interact button
    /// </summary>
    /// <param name="dir">Player (obj) Direction</param>
    /// <param name="timeHolding">Time between the pressed and released of input</param>
    public void Propulse(Vector3 dir, float timeHolding)
    {
        transform.localScale = Vector2.one / 1.2f;
        AddVelocity(dir, timeHolding);
    }

    /// <summary>
    /// Switch the velocity to the new one and increase the value if it's necessary
    /// </summary>
    /// <param name="dir">Player (obj) Direction</param>
    /// <param name="timeHolding">Time between the pressed and released of input</param>
    private void AddVelocity(Vector3 dir, float timeHolding)
    {
        m_rb.velocity = dir * m_velocityMultiplier * timeHolding;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        HitDirection(col.transform.up);
    }
}
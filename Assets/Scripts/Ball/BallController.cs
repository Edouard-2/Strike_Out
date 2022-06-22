using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //---------------------------Components---------------------------//
    public Rigidbody2D m_rb;
    public CircleCollider2D m_collider;

    //---------------------------Private---------------------------//
    private float m_velocityMultiplier = 2f;

    private float m_stepSpeed = 0.1f;
    private float m_speedBall = 1f;
    private float m_speedBallMax = 14;

    public bool m_isCatched = false;

    /// <summary>
    /// Check if the ball is catched
    /// </summary>
    /// <returns></returns>
    public BallController Able()
    {
        if (m_isCatched) return null;
        return this;
    }

    /// <summary>
    /// Change the direction of the ball when it's hit
    /// </summary>
    /// <param name="dir"> The direction pointing by the hit object </param>
    public void HitDirection(Vector3 dir)
    {
        //Changer la direction de la balle
        m_rb.velocity = dir * m_speedBall;
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
    public void Propulse(Vector3 dir)
    {
        transform.localScale = Vector2.one / 1.2f;
        AddVelocity(dir);
    }

    /// <summary>
    /// Switch the velocity to the new one and increase the value if it's necessary
    /// </summary>
    /// <param name="dir">Player (obj) Direction</param>
    /// <param name="timeHolding">Time between the pressed and released of input</param>
    private void AddVelocity(Vector3 dir)
    {
        m_rb.velocity = dir * m_velocityMultiplier * m_speedBall;
        AddSpeedBall();
    }

    private void FixedUpdate()
    {
        m_rb.velocity = m_rb.velocity.normalized * m_speedBall;
    }

    private void AddSpeedBall()
    {
        if (m_speedBall == m_speedBallMax) return;

        m_speedBall *= m_velocityMultiplier;
        m_speedBall += m_stepSpeed;

        if (m_speedBall > m_speedBallMax) m_speedBall = m_speedBallMax;
    }
}
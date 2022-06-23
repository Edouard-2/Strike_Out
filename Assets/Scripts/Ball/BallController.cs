using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //---------------------------Components---------------------------//
    public Rigidbody2D m_rb;
    public CircleCollider2D m_collider;

    //---------------------------Camera Property---------------------------//
    public Camera m_camera;
    public float m_durationShake;
    
    //---------------------------Private---------------------------//
    private float m_velocityMultiplier = 2f;

    private float m_stepSpeed = 0.1f;
    private float m_speedBall = 1f;
    private float m_speedBallMax = 15;
    private float m_initCameraZoom;
    
    private Coroutine m_coroutineShake;

    public bool m_isCatched;
    
    public Quaternion m_initRotation;
    
    private Coroutine m_coroutineZoom;

    private void Awake()
    {
        m_camera = Camera.main;
        m_initRotation = m_camera.transform.rotation;
        m_initCameraZoom = m_camera.orthographicSize;
    }

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
    /// Stop the ball when a player will shoot it
    /// </summary>
    public void StopBall()
    {
        ShakeCamera();
        ZoomCamera(4.8f, 0.5f);
        transform.localScale = Vector2.one * 0.5f;
        m_rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Propulse the ball when the player release the interact button
    /// </summary>
    /// <param name="dir">Player (obj) Direction</param>
    /// <param name="timeHolding">Time between the pressed and released of input</param>
    public void Propulse(Vector3 dir)
    {
        ZoomCamera(5, 0.07f);
        //transform.localScale = Vector2.one * 0.5f;
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
        m_speedBall = m_speedBallMax;
    }

    /// <summary>
    /// Zoom the camera when the pleyer catch the ball
    /// </summary>
    /// <param name="isZoom"> size zoom </param>
    private void ZoomCamera(float zoom,float durationZoom)
    {
        Debug.Log("Zoom");
        if (m_coroutineZoom != null) StopCoroutine(m_coroutineZoom);

        m_coroutineZoom = StartCoroutine(ZoomTrans(zoom, durationZoom));
    }
    
    IEnumerator ZoomTrans(float newZoom, float zoomDuration)
    {
        float time = 0;
        while (time < zoomDuration)
        {
            time += Time.deltaTime;
            m_camera.orthographicSize = Mathf.Lerp(m_initCameraZoom, newZoom, time / zoomDuration);
            m_initCameraZoom = m_camera.orthographicSize;
            yield return null;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.Instance.PlayHitWall();
        ShakeCamera();
    }

    private void ShakeCamera()
    {
        if (m_coroutineShake == null)
        {
            float valueShake = 1/ 7.5f * m_speedBall;
            m_camera.transform.DOShakeRotation(m_durationShake,valueShake,(int)valueShake,valueShake);
            m_coroutineShake = StartCoroutine(StopShakeCamera());
        }
    }

    IEnumerator StopShakeCamera()
    {
        float time = 0;
        while (time < m_durationShake)
        {
            time += Time.deltaTime;
            yield return null;
        }

        m_camera.transform.rotation = m_initRotation;
        m_coroutineShake = null;
    }

    public void ResetBall()
    {
        m_rb.velocity = Vector2.zero;
    }
}
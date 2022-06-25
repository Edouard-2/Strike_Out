using System.Collections;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Sponsor : MonoBehaviour
{
    public MasterPlayerController m_player;
    [FormerlySerializedAs("m_sprite")] public Transform m_spriteTransform;
    [SerializeField, Tooltip("Le particule system correspondant au sponsor")] public GameObject m_particuleBlock;
    private bool m_isActive;

    public void Init(MasterPlayerController player)
    {
        m_player = player;
    }

    protected virtual void FeedBack()
    {
        if (m_isActive) return;
        m_isActive = true;

        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer()
    {
        float time = 0;
        Vector3 initPosition = m_spriteTransform.position;
        Vector3 initScale = m_spriteTransform.localScale;
        
        while (Vector3.Distance(m_player.m_playerManager.transform.position,m_spriteTransform.position) > 0.1f)
        {
            Debug.Log(time);
            m_spriteTransform.position = Vector3.Lerp(initPosition, m_player.m_playerManager.transform.position, time*2);
            m_spriteTransform.localScale = Vector3.Lerp(initScale, Vector3.zero, time);
            time += Time.deltaTime;
            yield return null;
        }
        
        Destroy(m_spriteTransform.gameObject);
        
        Power();
        
        // Feed backs
        Instantiate(m_particuleBlock, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
    
    public abstract void Power();
    public abstract void Active();
}
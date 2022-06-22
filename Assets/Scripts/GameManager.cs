using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    
    //--------------------------Player----------------------------//
    [SerializeField, Tooltip("Position des différents joueurs au spawn")] private Transform[] m_listTransform = new Transform[2];
    [SerializeField, Tooltip("Position des différents joueurs au spawn")] private Transform[] m_listReady = new Transform[2];

    //--------------------------Ball----------------------------//
    [SerializeField, Tooltip("Position de spawn de la balle")] private Transform m_spawnBall;
    [SerializeField, Tooltip("Prefab de la ball")] private GameObject m_ballPrefab;
    
    //--------------------------Private----------------------------//
    private int m_indexSpwan = 0;
    
    public void OnPlayerJoin(PlayerInput playerInput)
    {
        playerInput.transform.position = m_listTransform[m_indexSpwan].position; 
        m_listReady[m_indexSpwan].gameObject.SetActive(false);
        m_indexSpwan++;
        if (m_indexSpwan > 1) SpawnBall();
    }

    private void SpawnBall()
    {
        
    }
}

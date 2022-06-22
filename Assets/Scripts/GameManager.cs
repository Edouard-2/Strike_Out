using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    //--------------------------Player----------------------------//
    private List<Transform> m_listPlayer = new List<Transform>();
    [SerializeField, Tooltip("Position des différents joueurs au spawn")] private List<Transform> m_listTransform;
    [SerializeField, Tooltip("Texte Ready pout lancer le jeu")] private List<ReadyInGame> m_listReady;

    //--------------------------Ball----------------------------//
    [SerializeField, Tooltip("Position de spawn de la balle")] private List<Transform> m_spawnBall;
    [SerializeField, Tooltip("Prefab de la ball")] private GameObject m_ballPrefab;
    
    //--------------------------Private----------------------------//
    private int m_indexSpwan;

    public void OnPlayerJoin()
    {
        StartCoroutine(DOPlayerJoin());
    }
    IEnumerator DOPlayerJoin()
    {
        yield return new WaitForSeconds(0.01f);
        
        Debug.Log(m_listPlayer.Count);
        
        m_listPlayer[m_indexSpwan].transform.position = m_listTransform[m_indexSpwan].position;
        m_listPlayer[m_indexSpwan].transform.rotation = m_listTransform[m_indexSpwan].rotation;

        m_listPlayer[m_indexSpwan].transform.localScale = Vector2.zero;
        
        m_listReady[m_indexSpwan].StartButton();
        
        StartCoroutine(WaitForPlayerSpawn(m_listPlayer[m_indexSpwan].transform.gameObject));

        m_indexSpwan++;
        if (m_indexSpwan > 1) StartCoroutine(SpawnBall());
    }

    public void AddPlayer(Transform player)
    {
        m_listPlayer.Add(player);
    }

    IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(2.1f);
        GameObject go = Instantiate(m_ballPrefab, m_spawnBall[Random.Range(0,2)].position, Quaternion.identity);
        StartCoroutine(SpawnGameObjectToSize(go));
    }

    IEnumerator WaitForPlayerSpawn(GameObject go)
    {
        yield return new WaitForSeconds(1.6f);
        StartCoroutine(SpawnGameObjectToSize(go));
    }
    
    /// <summary>
    /// Spawn a GameObject with a lerp between scale 0 and 1
    /// </summary>
    /// <param name="go"> the gameObject</param>
    /// <returns></returns>
    IEnumerator SpawnGameObjectToSize(GameObject go)
    {
        float time = 0;
        
        go.transform.localScale = Vector2.zero;
        Vector2 initScale = go.transform.localScale;

        while (time < 0.5f)
        {
            time += Time.deltaTime;
            go.transform.localScale = Vector2.Lerp(initScale, Vector2.one, time / 0.5f);
            yield return null;
        }
        go.transform.localScale = Vector2.one;
    }

    protected override string GetSingletonName()
    {
        return "GameManager";
    }
}

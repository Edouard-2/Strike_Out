using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : Singleton<GameManager>
{
    //--------------------------Player----------------------------//
    [Header("Player")] private List<PlayerManager> m_listPlayer = new List<PlayerManager>();

    [SerializeField, Tooltip("Position des différents joueurs au spawn")]
    private List<Transform> m_listTransform;

    [SerializeField, Tooltip("Texte Ready pout lancer le jeu")]
    public List<ReadyInGame> m_listReady;

    //--------------------------Goals----------------------------//
    [Header("Goal")] [SerializeField, Tooltip("Les deux Buts")]
    private List<Goal> m_goalList;

    [SerializeField, Tooltip("Particule d'explosion")]
    private VisualEffectAsset m_particleGoal;

    //--------------------------Ball----------------------------//
    [Header("Ball")] [SerializeField, Tooltip("Position de spawn de la balle")]
    private List<Transform> m_spawnBall;

    [SerializeField, Tooltip("Prefab de la ball")]
    private GameObject m_ballPrefab;

    //--------------------------Private----------------------------//
    private int m_indexSpawn;

    private GameObject m_ballInGame;

    private void Awake()
    {
        DataManager.Instance.m_masterPlayerList.ForEach(p => { p.m_playerSelecter.m_state = SelecterController.States.GAMEPLAY;});
    }

    public void OnPlayerJoin(MasterPlayerController player)
    {
        Debug.Log(m_listPlayer.Count);

        InitPlayerWhenSpawning(player);

        m_listReady[player.m_id].CancelButton();

        StartCoroutine(WaitForPlayerSpawn(player.m_playerManager.gameObject));

        m_indexSpawn++;
        if (m_indexSpawn > 1) StartCoroutine(SpawnBall());
    }

    public void StartReadyButton(int id)
    {
        m_listReady[id].StartButton();
    }

    private void InitPlayerWhenSpawning(MasterPlayerController player)
    {
        player.m_playerManager.transform.localScale = Vector2.zero;

        player.m_playerManager.m_goal = m_goalList[player.m_id];
        player.m_playerManager.InitGoalScript();

        player.m_playerManager.transform.position = m_listTransform[player.m_id].position;
        player.m_playerManager.transform.rotation = m_listTransform[player.m_id].rotation;
    }

    public void RespawnBall(Transform transform)
    {
        m_ballInGame.transform.position = transform.position;
        m_ballInGame.GetComponent<BallController>().ResetBall();
        //Instantiate(m_particleGoal, m_ballInGame.transform.position, Quaternion.identity);
        StartCoroutine(SpawnGameObjectToSize(m_ballInGame));
    }

    public void AddPlayer(PlayerManager player)
    {
        m_listPlayer.Add(player);
    }

    IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(2.1f);
        m_ballInGame = Instantiate(m_ballPrefab, m_spawnBall[Random.Range(0, 2)].position, Quaternion.identity);
        StartCoroutine(SpawnGameObjectToSize(m_ballInGame));
    }

    IEnumerator WaitForPlayerSpawn(GameObject go)
    {
        yield return new WaitForSeconds(1.1f);
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
            go.transform.localScale = Vector2.Lerp(initScale, Vector2.one * 0.5f, time / 0.5f);
            yield return null;
        }

        go.transform.localScale = Vector2.one * 0.5f;
    }

    protected override string GetSingletonName()
    {
        return "GameManager";
    }

    public void Win(PlayerManager mPlayerManager)
    {
        Debug.Log("Win");
    }
}
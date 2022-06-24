using UnityEngine;

public class Spid : Sponsor
{
    public override void Active()
    {
        Debug.Log(m_player.m_id);
        m_player.m_playerManager.m_playerController.m_speedMovement += 0.1f;
        Destroy(gameObject);
    }
}

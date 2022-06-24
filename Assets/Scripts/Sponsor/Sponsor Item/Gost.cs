using UnityEngine;

public class Gost : Sponsor
{
    public override void Active()
    {
        m_player.m_playerManager.m_playerInteraction.AddGhostBall();
        Destroy(gameObject);
    }
}
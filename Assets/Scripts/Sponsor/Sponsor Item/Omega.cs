using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omega : Sponsor
{
    public override void Active()
    {
        GameManager.Instance.m_ballInGame.GetComponent<BallManager>().ActiveOmega(m_player.m_id);
        Destroy(gameObject);
    }
}

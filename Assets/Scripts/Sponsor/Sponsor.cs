using UnityEngine;

public abstract class Sponsor : MonoBehaviour
{
    public MasterPlayerController m_player;

    public void Init(MasterPlayerController player)
    {
        m_player = player;
    }
    
    public abstract void Active();
}
using System.Collections.Generic;
using UnityEngine;

public class SponsorManager : Singleton<SponsorManager>
{
    [SerializeField, Tooltip("Panneau du joueur")] private List<SpriteRenderer> m_spriteRendererList = new List<SpriteRenderer>();
    private int id;
    
    protected override string GetSingletonName()
    {
        return "SponsorManager";
    }

    public void Join(MasterPlayerController player)
    {
        if (!SceneManager.Instance.CanPlay) return;
        
        if (player.m_id >= 2) return;
        
        m_spriteRendererList[player.m_id].color = Color.yellow;

        if(player.m_id == 1) MasterInputManager.Instance.gameObject.SetActive(false);
    }

    public void Ready(int id)
    {
        m_spriteRendererList[id].color = Color.green;
        if(DataManager.Instance.m_masterPlayerList.Count < 2) return;
        bool allReady = true;
        
        DataManager.Instance.m_masterPlayerList.ForEach(p => { if (!p.m_playerSelecter.m_isReady) {allReady = false;} });
        
        if(!allReady) return;
        DataManager.Instance.m_masterPlayerList.ForEach(p => { p.m_playerSelecter.m_state = SelecterController.States.NULL;});
        
        DataManager.Instance.m_masterPlayerList.ForEach(p =>
        {
            p.m_playerSelecter.m_spriteRendererSelecter.enabled = false;
            p.m_playerSelecter.m_spriteRendererSelecterShadow.enabled = false;
        });
        
        SceneManager.Instance.GoToScene(4);
    }
}
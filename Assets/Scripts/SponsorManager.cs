using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponsorManager : Singleton<SponsorManager>
{
    private List<SelecterController> m_selectersController = new List<SelecterController>();
    private SelecterController m_lastPlayer = null;
    public int m_id = 0;
    public void InitPlayerAfterJoin()
    {
        Debug.Log("Player Join !");
        m_id++;
        StartCoroutine(GetPlayer());
    }

    public void Add(SelecterController selecterController)
    {
        m_selectersController.Add(selecterController);
    }

    IEnumerator GetPlayer()
    {
        yield return new WaitForSeconds(0.01f);
        if(m_selectersController.Count > 0) m_lastPlayer = m_selectersController[m_selectersController.Count - 1];
        else Debug.LogWarning("Pas de player connecté !");
        
        Debug.Log(m_lastPlayer.name);
    }

    protected override string GetSingletonName()
    {
        return "SponsorManager";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameToMenuButton : MenuButton
{
    public override void Interact()
    {
        DataManager.Instance.m_masterPlayerList.ForEach(p=>{Destroy(p);});
        DataManager.Instance.m_masterPlayerList = new List<MasterPlayerController>();
        
        SceneManager.Instance.GoToScene(2);
    }
}

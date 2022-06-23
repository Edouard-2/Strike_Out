using System.Collections.Generic;

public class SponsorButton : MenuButton
{
    public override void Interact()
    {
        DataManager.Instance.m_masterPlayerList.ForEach(p=>{Destroy(p);});
        DataManager.Instance.m_masterPlayerList = new List<MasterPlayerController>();
        
        SceneManager.Instance.GoToScene(3);
    }
}
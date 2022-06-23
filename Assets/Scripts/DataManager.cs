using System.Collections.Generic;

public class DataManager : Singleton<DataManager>
{
    public List<MasterPlayerController> m_masterPlayerList = new List<MasterPlayerController>();

    public void Add(MasterPlayerController player)
    {
        m_masterPlayerList.Add(player);
    }
    
    protected override string GetSingletonName()
    {
        return "DataManager";
    }
}

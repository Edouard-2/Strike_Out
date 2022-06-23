using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SponsorMenu : UIManager
{
    [SerializeField, Tooltip("Line 1")] private List<SponsorButton> m_line1List = new List<SponsorButton>();
    [SerializeField, Tooltip("Line 1")] private List<SponsorButton> m_line2List = new List<SponsorButton>();
    
    private List<List<SponsorButton>> m_sponsorButtonList = new List<List<SponsorButton>>();

    private int m_lineButtonSelected = 0;
    private int m_rowButtonSelected = 0;
    private bool m_onPress = false;
    private bool m_onMove = false;

    private void Start()
    {
        m_sponsorButtonList.Add(m_line1List);
        m_sponsorButtonList.Add(m_line2List);
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].FirstSelected();
    }

    protected override void Up_Started(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        if (m_lineButtonSelected - 1 < 0) return;
        
        if(m_onPress)
        {
            m_onMove = true;
        }
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Unselected();
        m_lineButtonSelected--;
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Selected();
    }
    protected override void Down_Started(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        if (m_lineButtonSelected + 1 >= m_sponsorButtonList.Count) return;

        if (m_onPress)
        {
            m_onMove = true;
        }
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Unselected();
        m_lineButtonSelected++;
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Selected();
    }
    protected override void Left_Started(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        if (m_rowButtonSelected - 1 < 0) return;
        
        if(m_onPress)
        {
            m_onMove = true;
        }
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Unselected();
        m_rowButtonSelected--;
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Selected();
    }
    protected override void Right_Started(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        if (m_rowButtonSelected + 1 >= m_sponsorButtonList.Count) return;

        if (m_onPress)
        {
            m_onMove = true;
        }
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Unselected();
        m_rowButtonSelected++;
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Selected();
    }
    protected override void Select_Started(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        m_onPress = true;
        m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Pressed();
    }
    protected override void Select_Canceled(InputAction.CallbackContext ctx)
    {
        if (!SceneManager.Instance.CanPlay) return;
        if(m_onMove)
            m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Released();
        else
        {
            m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Released();
            m_sponsorButtonList[m_lineButtonSelected][m_rowButtonSelected].Interact();
        }
        m_onMove = false;
        m_onPress = false;
    }
    protected override void Back_Started(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Back_Canceled(InputAction.CallbackContext ctx)
    {
        
    }
}

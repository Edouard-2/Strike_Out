using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : UIManager
{
    [SerializeField, Tooltip("Menu Button List")] private List<MenuButton> m_menuButtonList = new List<MenuButton>();

    private int m_idButtonSelected = 0;

    private void Start()
    {
        m_menuButtonList[m_idButtonSelected].FirstSelected();
    }

    protected override void Up_Started(InputAction.CallbackContext ctx)
    {
        m_menuButtonList[m_idButtonSelected].Unselected();
        m_idButtonSelected--;
        m_idButtonSelected = m_idButtonSelected < 0 ? m_menuButtonList.Count - 1 : m_idButtonSelected;
        m_menuButtonList[m_idButtonSelected].Selected();
    }
    protected override void Down_Started(InputAction.CallbackContext ctx)
    {
        m_menuButtonList[m_idButtonSelected].Unselected();
        m_idButtonSelected++;
        m_idButtonSelected = m_idButtonSelected >= m_menuButtonList.Count ? 0 : m_idButtonSelected;
        m_menuButtonList[m_idButtonSelected].Selected();
    }
    protected override void Select_Started(InputAction.CallbackContext ctx)
    {
        m_menuButtonList[m_idButtonSelected].Pressed();
    }
    protected override void Select_Canceled(InputAction.CallbackContext ctx)
    {
        m_menuButtonList[m_idButtonSelected].Released();
    }
    protected override void Back_Started(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Back_Canceled(InputAction.CallbackContext ctx)
    {
        
    }
}

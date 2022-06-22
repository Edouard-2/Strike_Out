using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : UIManager
{
    [SerializeField, Tooltip("Menu Button List")] private List<GameObject> m_menuButtonList = new List<GameObject>();
    
    private int m_selectedAnimation = Animator.StringToHash("Selected");
    private int m_pressedAnimation = Animator.StringToHash("Pressed");
    private int m_releasedAnimation = Animator.StringToHash("Released");

    private int m_idButtonSelected = 0;

    private void Start()
    {
        m_menuButtonList[m_idButtonSelected].GetComponent<Animator>().SetTrigger(m_selectedAnimation);
    }

    protected override void Up_Started(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Up_Canceled(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Down_Started(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Down_Canceled(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Select_Started(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Select_Canceled(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Back_Started(InputAction.CallbackContext ctx)
    {
        
    }
    protected override void Back_Canceled(InputAction.CallbackContext ctx)
    {
        
    }
}

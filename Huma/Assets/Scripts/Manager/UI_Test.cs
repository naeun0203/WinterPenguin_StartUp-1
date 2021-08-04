using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test : MonoBehaviour
{
    [SerializeField]
    public Player player;

    public UIPanel _basePanel;
    public UIPanel _currentPanel;

    public UIPanel HUD, subMenu, settingMenu, slotMachine;

    #region SingleTon
    public static UI_Test instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        BasePanel(HUD);

        TriggerClosePanel(settingMenu);
        TriggerClosePanel(subMenu);
        TriggerClosePanel(slotMachine);

        TriggerOpenPanel(HUD);
    }

    public void Update()
    {
        if (_currentPanel != null) _currentPanel.UpdateBehavior();
    }

    public void BasePanel(UIPanel panel)
    {
        _basePanel = panel;
        panel.OpenBehavior();
    }

    public void TriggerPanelTransition(UIPanel panel)
    {
        
        TriggerOpenPanel(panel);
    }

    public void TriggerOpenPanel(UIPanel panel) 
    {
        if(_currentPanel == HUD)
        {
            _currentPanel = panel;
        }
        else if(_currentPanel == subMenu && panel == settingMenu)
        {
            _currentPanel = panel;
        }
        else if(_currentPanel != null )
        {
            TriggerClosePanel(_currentPanel);
        }
        _currentPanel = panel;
        panel.OpenBehavior();
    }
    public void TriggerClosePanel(UIPanel panel) 
    {
        panel.CloseBehavior();
    }
}

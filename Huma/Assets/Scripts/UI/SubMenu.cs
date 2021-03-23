using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : UIPanel
{
    public GameObject SettingMenu;
    public Button continueButton, retryButton, settingButton, giveupButton;
    public override void OpenBehavior()
    {
        base.OpenBehavior();

        continueButton.onClick.AddListener(() => ContinueButtonClicked());
        settingButton.onClick.AddListener(() => SettingButtonClicked());
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UI_Test.instance._currentPanel==this) 
        { 
            UI_Test.instance.TriggerClosePanel(this);
            UI_Test.instance._currentPanel = UI_Test.instance.HUD;

            Time.timeScale = 1;
        }
    }

    void ContinueButtonClicked()
    {
        UI_Test.instance._currentPanel = UI_Test.instance.HUD;

        UI_Test.instance.TriggerClosePanel(this);

        Time.timeScale = 1;
    }

    void SettingButtonClicked()
    {
        UIPanel targetPanel = UI_Test.instance.settingMenu;
        UI_Test.instance.TriggerOpenPanel(targetPanel);
    }
}

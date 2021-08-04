using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : UIPanel
{
    //SettingMenu settingMenu;
    public override void OpenBehavior()
    {
        base.OpenBehavior();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            UI_Test.instance._currentPanel = UI_Test.instance.subMenu;

            UI_Test.instance.TriggerClosePanel(this); 
        }
    }
}

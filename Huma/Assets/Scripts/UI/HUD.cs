using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : UIPanel
{
    private int level = 1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UI_Test.instance._currentPanel == this)
        {
            UIPanel targetPanel = UI_Test.instance.subMenu;
            UI_Test.instance.TriggerOpenPanel(targetPanel);

            Time.timeScale = 0;
        }
        if (level != UI_Test.instance.player.level)
        {
            UIPanel targetPanel = UI_Test.instance.slotMachine;
            UI_Test.instance.TriggerOpenPanel(targetPanel);

            level = UI_Test.instance.player.level;
            Time.timeScale = 0;
        }
    }
}

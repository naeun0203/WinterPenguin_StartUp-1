using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_StageSelect : MonoBehaviour
{
    public GameObject SettingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Close()
    {
        SettingsPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (SettingsPanel.activeSelf)
                SettingsPanel.SetActive(false);

            else
                SettingsPanel.SetActive(true);
        }
    }
    public void GameExit()
    {
        Application.Quit();
    }
}

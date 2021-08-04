using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject subMenu;
    public GameObject settingMenu;

    private void Start()
    {
        subMenu.SetActive(false);
        settingMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (subMenu.activeSelf && settingMenu.activeSelf == false)
            {
                subMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else if (subMenu.activeSelf == false)
            {
                subMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (settingMenu.activeSelf)
            {
                settingMenu.SetActive(false);
            }
        }
    }
    public void SettingMenuActivation()
    {
        settingMenu.SetActive(true);
    }
    public void ContinueActivation()
    {
        subMenu.SetActive(false);
        Time.timeScale = 1;
    }
}

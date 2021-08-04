//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.12.21
// Scrit Purpose : ...
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject SubMenu;
    public GameObject SettingMenu;
    public GameObject ClearPanel;
    public GameObject SlotMachinePanel;
    [SerializeField]
    private Player player;

    private int level = 1;

    #region Singleton
/*    private static UIManager _instance;
    public static UIManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }*/
    #endregion

    private void Start()
    {
        HUD.gameObject.SetActive(true);
        SubMenu.SetActive(false);
        SettingMenu.SetActive(false);
        ClearPanel.SetActive(false);
        SlotMachinePanel.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SubMenu.activeSelf == false)
            {
                SubMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (SubMenu.activeSelf && SettingMenu.activeSelf == false)
            {
                SubMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else if (SettingMenu.activeSelf)
            {
                SettingMenu.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SettingMenu.SetActive(false);
            Time.timeScale = 1;
        }
        if (level != player.level)
        {
            SlotMachinePanel.SetActive(true);
            level = player.level;
            Time.timeScale = 0;
        }
    }
    public void SettingMenuActivation()
    {
        SettingMenu.SetActive(true);
    }
    public void ContinueActivation()
    {
        SubMenu.SetActive(false);
        Time.timeScale = 1;
    }
}

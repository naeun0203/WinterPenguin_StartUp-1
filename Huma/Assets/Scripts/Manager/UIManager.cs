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
    [SerializeField]
    private int level = 1;

/*    [SerializeField]
    private Canvas startingCanvas;
    [SerializeField]
    private Canvas[] _uiBase;*/

    /*    private Canvas currentCanvas;

        private readonly Stack<UIBase> history = new Stack<UIBase>();*/

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


/*    public static T GetCanvas<T>() where T: UIBase
    {
        for(int i=0;i<_instance._uiBase.Length; i++)
        {
            if(_instance._uiBase[i] is T tCanvas)
            {
                return tCanvas;
            }
        }
        return null;
    }

    public static void Show<T>(bool remember = true) where T: UIBase
    {
        for(int i = 0; i < _instance._uiBase.Length; i++)
        {
            if(_instance._uiBase[i] is T)
            {
                if(_instance.currentCanvas != null)
                {
                    if (remember)
                    {
                        _instance.history.Push(_instance.currentCanvas);
                    }

                    _instance.currentCanvas.Hide();
                }

                _instance._uiBase[i].Show();

                _instance.currentCanvas = _instance._uiBase[i];
            }
        }
    }

    public static void Show(UIBase uiBase, bool remember = true)
    {
        if(_instance.currentCanvas != null)
        {
            if (remember)
            {
                _instance.history.Push(_instance.currentCanvas);
            }
            _instance.currentCanvas.Hide();
        }
        uiBase.Show();

        _instance.currentCanvas = uiBase;
    }

    public static void ShowLast()
    {
        if(_instance.history.Count != 0)
        {
            Show(_instance.history.Pop(), false);
        }
    }*/
    private void Start()
    {
        HUD.gameObject.SetActive(true);
        SubMenu.SetActive(false);
        SettingMenu.SetActive(false);
        ClearPanel.SetActive(false);
        SlotMachinePanel.SetActive(false);
        //level = player.level;
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

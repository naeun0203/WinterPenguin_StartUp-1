﻿//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.12.21
// Scrit Purpose : ...
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager instance = null;
    public GameObject SlotMachinePanel;
    public static UI_Manager Instance
        
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    public void ShowSlotMachine()
    {
        SlotMachinePanel.SetActive(true);
    }
    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

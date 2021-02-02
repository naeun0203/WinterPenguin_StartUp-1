﻿//***************************************
// EDITOR : CHA Hee Gyoung
// LAST EDITED DATE : 2020.12.21
// Script Purpose : game logic
//***************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM_InStage : MonoBehaviour
{
    public GameObject escPanel;
    public GameObject clearPanel;
    public GameObject roulettePanel;
    public GameObject slotmachinePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Resume()
    {
        escPanel.SetActive(false);
    }
    public void SlotMachine()
    {
        escPanel.SetActive(false);
        slotmachinePanel.SetActive(true);
    }
    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }
    public void Enhancement()
    {
        escPanel.SetActive(false);
        roulettePanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (escPanel.activeSelf)
                escPanel.SetActive(false);

            else
                escPanel.SetActive(true);
        }
        if (Input.GetButtonDown("Jump"))
        {
            clearPanel.SetActive(true);
        }
    }
    
}

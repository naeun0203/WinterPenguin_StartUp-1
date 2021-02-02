//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.01.27
// Scrit Purpose : ...
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject Intro;
    public GameObject Stage;
    void Start()
    {
        optionMenu.SetActive(false);
        Stage.SetActive(false);
        Intro.SetActive(true);
    }
    void Update()
    {
        if (Intro.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (optionMenu.activeSelf == true)
                {
                    CloseMenu();
                }
                else
                {
                    SetOptionMenuActivation();
                }
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                Intro.SetActive(false);
                Stage.SetActive(true);
            }
        }
    }
    public void CloseMenu()
    {
        optionMenu.SetActive(false);
    }
    public void SetOptionMenuActivation()
    {
        optionMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

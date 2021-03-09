//***************************************
// EDITOR : CHA Hee Gyoung
// LAST EDITED DATE : 2020.12.21
// Script Purpose : game logic
//***************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    /*
    private static Gamemanager instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    */
    
    public static int StageNumber = 1;
    public int CurrentMonster;
    public static int zombieCount = 0;
    public static int devildogCount = 0;
    public static int spiterCount = 0;
    public static int tankerCount = 0;
    public List<Transform> MonsterList = new List<Transform>();
    /*
    public static Gamemanager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    */
    public void Stage1Select()
    {
        StageNumber = 1;

        zombieCount = 600;

    }
    public void Stage2Select()
    {
        StageNumber = 2;

        zombieCount = 600;
        devildogCount = 150;
    }
    public void Stage3Select()
    {
        StageNumber = 3;

        zombieCount = 600;
        devildogCount = 200;
        spiterCount = 40;
    }
    public void Stage4Select()
    {
        StageNumber = 4;

        zombieCount = 600;
        devildogCount = 200;
        spiterCount = 80;
        tankerCount = 8;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomReward : MonoBehaviour
{
    [SerializeField]
    private UIManager UImanagement;
    public GameObject ApplyButton;
    [SerializeField]
    private DBManager_Player PlayerDB;
    [SerializeField]
    private Character_SuHyeon SuHyeon_get;
    void Start()
    {
        UImanagement = GameObject.Find("UIManager").GetComponent<UIManager>();
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        SuHyeon_get = GameObject.Find("Kim").GetComponent<Character_SuHyeon>();
    }
    
    public int ResultCase;
    /// <summary>
    /// 
    /// </summary>
    public void StartButton()
    {
        
        int number = Random.Range(0, 10000);
        if (number>=0&& number < 300)
        {
            ResultCase = 1;
        }
        if (number >= 300 && number < 2000)
        {
            ResultCase = 2;
        }
        if (number >= 2000 && number < 3600)
        {
            ResultCase = 3;
        }
        if (number >= 3600 && number < 5200)
        {
            ResultCase = 4;
        }
        if (number >= 5200 && number < 6800)
        {
            ResultCase = 5;
        }
        
        if (number >= 6800 && number < 8400)
        {
            ResultCase = 6;
        }
        if (number >= 8400 && number < 9999)
        {
            ResultCase = 7;
        }
        
    }
    public void Apply()
    {
        switch (ResultCase)
        {
            case 1:
                //꽝
                UImanagement.SlotMachinePanel.SetActive(false);
                Debug.Log("perfect!");
                break;
            case 2:
                //공격력 +10
                SuHyeon_get.AtkDamage += 10;
                UImanagement.SlotMachinePanel.SetActive(false);
                break;
            case 3:
                //공격 속도 +10%
                SuHyeon_get.AtkSpeed += SuHyeon_get.AtkSpeed / 10f;
                UImanagement.SlotMachinePanel.SetActive(false);
                break;
            case 4:
                // 흡혈 +10%, 공격력+1
                SuHyeon_get.BloodSucking += SuHyeon_get.BloodSucking / 10f;
                SuHyeon_get.AtkDamage += 1;
                UImanagement.SlotMachinePanel.SetActive(false);
                break;
            case 5:
                //공격 가능 몬스터 수+1, 공격력+1
                SuHyeon_get.AtkCount += 1;
                SuHyeon_get.AtkDamage += 1;
                UImanagement.SlotMachinePanel.SetActive(false);
                break;
            case 6:
                //치명타 확률 10% 증가, 공격력1증가
                
                if (SuHyeon_get.CriticalProb <= 90)
                {
                    SuHyeon_get.CriticalProb += SuHyeon_get.CriticalProb / 10f;
                }
                else SuHyeon_get.CriticalProb = 100f;
                SuHyeon_get.AtkDamage += 1;
                UImanagement.SlotMachinePanel.SetActive(false);
                break;
            case 7:
                //치명타 확률 100% 증가, 공격력1증가

                SuHyeon_get.CriticalProb = 100f;
                SuHyeon_get.AtkDamage += 1;
                UImanagement.SlotMachinePanel.SetActive(false);
                break;

        }
    }

    public void ShowApplyButton()
    {
        ApplyButton.SetActive(true);
    }
}

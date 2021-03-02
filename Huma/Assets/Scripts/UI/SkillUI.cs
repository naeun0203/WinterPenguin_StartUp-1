//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.01.19
// Scrit Purpose : ...
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private Image skillImage;
    public TextMeshProUGUI skillText;

    [SerializeField]
    private DBManager_Player PlayerDB;
    public float cooldown;
    //public float skillCoolTime;
    [SerializeField]
    private float currentCoolTime;

    bool canUseSkill = false;
    public void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        skillImage.fillAmount = 0;
        
        StartCoroutine(DataSet());
    }

    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            if (PlayerDB.isLoaded)
            {
                cooldown = PlayerDB.skillCoolTime;

                DataLoading = false;
            }
            yield return null;
        }
        currentCoolTime = cooldown;
        yield return null;
    }

    private void Update()
    {
        cooldown = PlayerDB.skillCoolTime;
        Ability();
    }
    private void Ability()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canUseSkill)
        {
            skillImage.fillAmount = 0;
            canUseSkill = false;
        }
        if (canUseSkill == false)
        {
            //skillImage.fillAmount += 1 * Time.smoothDeltaTime / cooldown;

            if(currentCoolTime > 0)
            {
                skillText.text = "" + Mathf.CeilToInt(currentCoolTime);
            }
            //skillText.text = "" + currentCoolTime.ToString("N1");
            currentCoolTime -= Time.smoothDeltaTime;

            if(skillImage.fillAmount == 1)
            {
                skillText.text = "";
                canUseSkill = true;
                currentCoolTime = cooldown;
            }
        }
    }
}
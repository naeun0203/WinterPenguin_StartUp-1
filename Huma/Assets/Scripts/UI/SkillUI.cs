//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.01.19
// Scrit Purpose : ...
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Image skillImage;
    public Text skillText;

    private DBManager_Player PlayerDB;
    public float cooldown;
    private float currentCoolTime;
    bool canUseSkill = false;
    private void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        cooldown = PlayerDB.skillCoolTime;
        skillImage.fillAmount = 0;
        currentCoolTime = cooldown;
    }
    void Update()
    {
        Ability();
    }
    void Ability()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canUseSkill)
        {
            skillImage.fillAmount = 0;
            canUseSkill = false;
        }
        if (canUseSkill == false)
        {
            skillImage.fillAmount += 1 * Time.smoothDeltaTime / cooldown;

            if(currentCoolTime > 0)
            {
                skillText.text = "" + Mathf.CeilToInt(currentCoolTime);
            }
            //skillText.text = "" + currentCoolTime.ToString("N1");
            currentCoolTime -= Time.deltaTime;

            if(skillImage.fillAmount == 1)
            {
                skillText.text = "";
                canUseSkill = true;
                currentCoolTime = cooldown;
            }
        }
    }
}
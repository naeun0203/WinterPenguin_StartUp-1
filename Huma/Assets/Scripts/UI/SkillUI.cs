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

    public bool canUseSkill = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && canUseSkill)
        {
            skillImage.fillAmount = 0;
            canUseSkill = false;
        }
        if (canUseSkill == false)
        {
            currentCoolTime -= Time.deltaTime;
            
            if (currentCoolTime >= 0)
            {
                skillImage.fillAmount += 1 * Time.smoothDeltaTime / cooldown;
                skillText.text = "" + Mathf.CeilToInt(currentCoolTime);
            }
            if (skillImage.fillAmount == 1)
            {
                skillText.text = "";
                canUseSkill = true;
                currentCoolTime = cooldown;
            }
        }

    }
}
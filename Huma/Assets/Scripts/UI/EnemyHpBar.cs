//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.02.08
// Scrit Purpose : Enemy HealthBar
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Diagnostics;

public class EnemyHpBar : MonoBehaviour
{
    private Transform cam;

    private MonsterBase monster;
    private DBManager_Monster MonsterDB;

    public Image hpBarImage;
    public Transform hpbarPivot;
    public float activeTime = 3f;

    [SerializeField]
    private float currentHp;
    private float maxHp;
    private float ex_Hp;

    [SerializeField]
    private float timer;
    public float lerpSpeed = 2.5f;

    private Vector3 lookPosition;

    public bool timerStart;
    public void Start()
    {
        monster = GetComponentInParent<MonsterBase>();
        MonsterDB = GameObject.Find("DBManager").GetComponent<DBManager_Monster>();

        cam = Camera.main.transform;

        timerStart = false;
        hpBarImage.fillAmount = 1;
        //hpbarPivot.gameObject.SetActive(false);
        StartCoroutine(DataSet());
    }
    #region DataSet
    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            for (int i = 0; i < MonsterDB.monsterDB.Length; i++)
            {
                if (MonsterDB.monsterDB[i].name == monster.CurrentTribe.ToString())
                {
                    maxHp = MonsterDB.monsterDB[i].hp;

                    DataLoading = false;
                }
            }
            yield return null;
        }
        currentHp = maxHp;
        ex_Hp = currentHp;
        yield return null;
    }
    #endregion
    private void Update()
    {
        currentHp = monster.HP;
        if (currentHp != ex_Hp)
        {
            ex_Hp = currentHp;
            resetTime();
            timerStart = true;
        }
        if (timerStart)
        {
            timer += Time.deltaTime;
            hpbarPivot.gameObject.SetActive(true);
            hpBarImage.fillAmount = Mathf.Lerp(hpBarImage.fillAmount, currentHp / maxHp, Time.deltaTime * lerpSpeed);
            if (timer > activeTime)
            {
                resetTime();
                //hpbarPivot.gameObject.SetActive(false);
            }
        }
    }
    public void resetTime()
    {
        timerStart = false;
        timer = 0;
    }
    private void LateUpdate()
    {
        lookPosition = new Vector3(hpbarPivot.transform.position.x, cam.position.y, cam.position.z);
        hpbarPivot.transform.LookAt(lookPosition);
    }
}

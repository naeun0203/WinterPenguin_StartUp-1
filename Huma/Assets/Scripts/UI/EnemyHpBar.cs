//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.02.08
// Scrit Purpose : Enemy HealthBar
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    private Transform cam;

    [SerializeField]
    private MonsterBase monster;
    [SerializeField]
    private DBManager_Monster MonsterDB;

    public Image hpBarImage;
    public Transform hpbarPivot;
    public float activeTime = 3f;

    [SerializeField]
    private float currentHp;
    public float maxHp;
    [SerializeField]
    private float timer = 0f;

    public float lerpSpeed = 2.5f;

    private Vector3 lookPosition;
    public void Start()
    {
        monster = GetComponentInParent<MonsterBase>();
        MonsterDB = GameObject.Find("DBManager").GetComponent<DBManager_Monster>();

        cam = Camera.main.transform;

        //maxHp = monster.HP;
        //currentHp = maxHp;

        hpBarImage.fillAmount = 1;
        hpbarPivot.gameObject.SetActive(false);
        StartCoroutine(DataSet());
    }
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
        yield return null;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        currentHp = monster.HP;

        if (maxHp != currentHp)
        {
            timer = 0;
            hpbarPivot.gameObject.SetActive(true);
            hpBarImage.fillAmount = Mathf.Lerp(hpBarImage.fillAmount, currentHp / maxHp, Time.deltaTime * lerpSpeed);
            //currentHp = maxHp;
        }
        if (timer >= activeTime)
        {
            //hpbarPivot.gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        lookPosition = new Vector3(hpbarPivot.transform.position.x, cam.position.y, cam.position.z);
        hpbarPivot.transform.LookAt(lookPosition);
    }
}

//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.01.19
// Script Purpose : Monster_Spiter Attack
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiterAttack : MonoBehaviour
{
    private float nextTime = 0.0f;
    public float AttackSpeed = 2.0f;

    public Transform SpiterMouth;
    MonsterManager monManager;

    void Start()
    {
        monManager = this.gameObject.GetComponent<MonsterManager>();
    }
    void Update()
    {
        if (monManager.isAttack == true)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + AttackSpeed;
                //ObjectPoolManager.Instance.pool.Pop().transform.position = SpiterMouth.transform.position;
            }
        }
    }
}

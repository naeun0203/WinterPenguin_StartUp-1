﻿//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.01.19
// Script Purpose : Zombie Attack
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float AttackSpeed = 2.0f;
    private float nextTime = 0.0f;

    public float Damage = 1.0f;
    // Start is called before the first frame update
    MonsterManager monManager;
    void Start()
    {
        monManager = this.gameObject.GetComponent<MonsterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (monManager.isAttack == true)
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + AttackSpeed;
                Attack();
            }
        }
    }

    private void Attack()
    {

    }
}

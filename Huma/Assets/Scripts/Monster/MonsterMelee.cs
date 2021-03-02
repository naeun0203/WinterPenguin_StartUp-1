//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.22
// Script Purpose : Monster_nav
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMelee : MonsterBase
{
    public GameObject meleeAtkArea;
    protected Vector3 Look;
    public float CurrentHP;
    void Start()
    {
        base.Start();
        AttackCoolTimeCacl = AttackSpeed;

        StartCoroutine(FSM());
        StartCoroutine(ResetAtkArea());
    }
    void Update()
    {
        CurrentHP = HP;
        if (HP > 0)
        {
            Look = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            transform.LookAt(Look);
        }
    }



    IEnumerator ResetAtkArea()
    {
        while (true)
        {
            yield return null;
            if (!meleeAtkArea.activeInHierarchy && CurrentState == State.Attack)
            {
                yield return new WaitForSeconds(AttackSpeed);
                meleeAtkArea.SetActive(true);
            }
        }
    }

    protected virtual IEnumerator FSM()
    {
        yield return null;

        while (true)
        {
            yield return StartCoroutine(CurrentState.ToString());
        }
    }
    protected virtual IEnumerator Idle()
    {
        yield return null;

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Anim.SetTrigger("Idle");
        }

        if (CanAtkStateFun())
        {
            if (canAtk)
            {
                CurrentState = State.Attack;
            }
            else
            {
                CurrentState = State.Idle;
            }
        }
        else
        {
            CurrentState = State.Move;
        }
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
        //Atk
        this.nvAgent.isStopped = true;
        this.nvAgent.updatePosition = false;
        this.nvAgent.updateRotation = false;
        this.nvAgent.velocity = Vector3.zero;
        this.nvAgent.isStopped = false;
        canAtk = false;

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Anim.SetTrigger("Attack");
        }

        //yield return Delay500;
        CurrentState = State.Idle;
    }

    protected virtual IEnumerator Move()
    {
        yield return null;
        this.nvAgent.ResetPath();
        this.nvAgent.isStopped = false;
        this.nvAgent.updatePosition = true;
        this.nvAgent.updateRotation = true;

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
        {
            Anim.SetTrigger("Moving");
        }

        if (CanAtkStateFun() && canAtk)
        {
            CurrentState = State.Attack;

        }
        else
        {
            this.nvAgent.SetDestination(Player.transform.position);
        }
    }
}

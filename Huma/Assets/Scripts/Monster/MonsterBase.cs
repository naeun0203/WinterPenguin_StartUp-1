//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.22
// Script Purpose : Monster_Base
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    public enum Tribe { Zombie, DevliDog, Spiter };
    public enum State { Idle, Move, Attack };

    public float HP = 100f;
    public float Damage = 10f;
    public float AttackRange = 1.5f;
    public float moveSpeed = 9f;
    public float AttackSpeed = 2f;

    protected float AttackCoolTimeCacl = 2f;
    protected bool canAtk = true;
    public bool isAttack = false;

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance;

    protected Rigidbody rb;


    public Tribe CurrentTribe = Tribe.Zombie;
    public State CurrentState = State.Idle;

    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        nvAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CalcCoolTime());
        StartCoroutine(CheckStateForActon());
        this.nvAgent.stoppingDistance = AttackRange;
        this.nvAgent.speed = moveSpeed;
    }
    protected bool CanAtkStateFun()
    {

        Vector3 targetDir = new Vector3(Player.transform.position.x - transform.position.x, 0f, Player.transform.position.z - transform.position.z);

        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out RaycastHit hit, 30f);
        distance = Vector3.Distance(Player.transform.position, transform.position);

        if (hit.transform == null)
        {
            return false;
        }

        if (hit.transform.CompareTag("Player") && distance <= AttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected virtual IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                AttackCoolTimeCacl -= Time.deltaTime;
                if (AttackCoolTimeCacl <= 0)
                {
                    AttackCoolTimeCacl = AttackSpeed;
                    canAtk = true;
                }
            }
        }
    }

    IEnumerator CheckStateForActon()
    {
        while (true)
        {
            switch (CurrentState)
            {
                case State.Idle:

                case State.Move:
                    this.nvAgent.isStopped = false;
                    isAttack = false;

                    break;
                case State.Attack:
                    isAttack = true;
                    break;

            }
            yield return null;
        }
    }
}

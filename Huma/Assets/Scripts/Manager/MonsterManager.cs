//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.01.19
// Script Purpose : Monster Moving, Attack Manager
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    public enum CurrenetState { Idle, Attack, Dead, Move };
    public CurrenetState CurState = CurrenetState.Move;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;

    public float MoveDist = 15.0f;
    public float AttackDist = 10.0f;
    public float Health = 100;

    private bool isDead = false;
    public bool isAttack = false;

    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForActon());

    }
    void Update()
    {
        if (Health == 0)
        {
            isDead = true;
        }
        transform.LookAt(playerTransform.position);
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (dist <= AttackDist)
            {
                CurState = CurrenetState.Attack;
            }
            else if (dist <= MoveDist)
            {
                CurState = CurrenetState.Move;
            }
            else
            {
                CurState = CurrenetState.Idle;
            }
        }
    }

    IEnumerator CheckStateForActon()
    {
        while (!isDead)
        {
            switch (CurState)
            {
                case CurrenetState.Idle:
                    //this.nvAgent.isStopped = true;       
                    this.nvAgent.velocity = Vector3.zero;
                    isAttack = false;
                    break;
                case CurrenetState.Attack:
                    isAttack = true;
                    break;
                case CurrenetState.Move:
                    this.nvAgent.isStopped = false;
                    nvAgent.destination = playerTransform.position;
                    isAttack = false;
                    break;
            }
            yield return null;
        }
    }
}

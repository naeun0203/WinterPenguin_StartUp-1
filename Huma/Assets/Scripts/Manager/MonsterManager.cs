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
    public enum CurrenetState {Move, Attack, Dead};
    public CurrenetState CurState = CurrenetState.Move;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;

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
        /*
        Vector3 Target = playerTransform.position - transform.position;
        Target.Normalize();
        Quaternion q = Quaternion.LookRotation(Target);
        transform.rotation = q;
        */
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
            else
            {
                CurState = CurrenetState.Move;
            }
        }
    }

    IEnumerator CheckStateForActon()
    {
        while (!isDead)
        {
            switch (CurState)
            {
                case CurrenetState.Move:
                    this.nvAgent.isStopped = false;
                    nvAgent.destination = playerTransform.position;
                    isAttack = false;
                    break;
                case CurrenetState.Attack:
                    isAttack = true;
                    break;

            }
            yield return null;
        }
    }
}

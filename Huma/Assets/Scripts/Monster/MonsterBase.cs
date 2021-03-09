//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.22
// Script Purpose : Monster_Base
//******************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    public enum Tribe { Zombie, DevilDog, Spiter, Tanker };
    public enum State { Idle, Move, Attack, Dead };

    [SerializeField] private DBManager_Monster MonsterData;
    [SerializeField] private GameObject Monster;

    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackSpeed;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected float AttackRadius;
    [SerializeField] protected float BloodSucking;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float NumberOfTargets;
    [SerializeField] protected float skillCoolTime;
    [SerializeField] protected float EXP;



    protected float AttackCoolTimeCacl = 2f;
    protected bool canAtk = true;
    public bool isAttack = false;
    protected bool dead = false; 

    protected Gamemanager gamemanager; 
    protected GameObject Player;
    protected Player player;
    protected NavMeshAgent nvAgent;
    protected Animator Anim;
    protected Vector3 pushDirection;

    protected float distance;

    protected Rigidbody rb;

    public Tribe CurrentTribe = Tribe.Zombie;
    public State CurrentState = State.Idle;
    protected bool knockBack = false;

    private float hp;
    protected void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>(); 
        MonsterData = GameObject.Find("DBManager").GetComponent<DBManager_Monster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player = GameObject.FindGameObjectWithTag("Player");

        nvAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();

        StartCoroutine(CalcCoolTime());
        StartCoroutine(CheckStateForActon());
        StartCoroutine(DataSet());
        this.nvAgent.stoppingDistance = AttackRange;
        this.nvAgent.speed = moveSpeed;
    }

    protected IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            for(int i = 0; i < MonsterData.monsterDB.Length; i++)
            {
                if (MonsterData.monsterDB[i].name == CurrentTribe.ToString())
                {
                    HP = MonsterData.monsterDB[i].hp;
                    Damage = MonsterData.monsterDB[i].damage;
                    AttackSpeed = MonsterData.monsterDB[i].attackSpeed;
                    AttackRange = MonsterData.monsterDB[i].attackRange;
                    AttackRadius = MonsterData.monsterDB[i].attackRadius;
                    BloodSucking = MonsterData.monsterDB[i].bloodSucking;
                    moveSpeed = MonsterData.monsterDB[i].moveSpeed;
                    NumberOfTargets = MonsterData.monsterDB[i].NumberOfTargets;
                    skillCoolTime = MonsterData.monsterDB[i].skillCoolTime;
                    EXP = MonsterData.monsterDB[i].EXP;

                    DataLoading = false;
                }
            }
            yield return null;
        }

        nvAgent.speed = moveSpeed;
        nvAgent.stoppingDistance = AttackRange;
        yield return null;
    }
    #region HP, EXP
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                dead = true;
                CurrentState = State.Dead;
                player.EXP += EXP;
                this.nvAgent.isStopped = true;
                this.nvAgent.speed = 0;

                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gamemanager.CurrentMonster -= 1;

                Anim.SetTrigger("Death");
                Invoke("Death", 2.0f);
            }
        }
    }
    void Death()
    {
        rb.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public float HpChanged(float damage)
    {
        rb = GetComponent<Rigidbody>();
        HP += damage;
       if(HP > 0)
        {
            knockBack = true;
            StartCoroutine(KnockBack());
        }
        return HP;
    }
    #endregion

    #region KnockBack
    protected void FixedUpdate()
    {
        pushDirection = (Player.transform.position - transform.position).normalized;
        if(!knockBack)
        {
            FreezeVelocity();
        }
    }

    protected IEnumerator KnockBack()
    {
        CurrentState = State.Idle;
        this.nvAgent.isStopped = true;
        this.nvAgent.speed = 0;

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            Anim.SetTrigger("Hit");
        }
        rb.velocity = pushDirection * -20;

        yield return new WaitForSeconds(0.3f);
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.2f);
        rb.isKinematic = false;

        this.nvAgent.isStopped = false;
        this.nvAgent.speed = moveSpeed;
        knockBack = false;
    }
    #endregion

    void FreezeVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
                if (AttackCoolTimeCacl <= 0 && !knockBack && !dead)
                {
                    AttackCoolTimeCacl = AttackSpeed;
                    canAtk = true;
                }
            }
        }
    }

    IEnumerator CheckStateForActon()
    {
        while (!dead)
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
                    if(CurrentTribe == Tribe.Spiter)
                    {
                        Damage = 0;
                    }

                    break;

            }
            yield return null;
        }
    }
}

/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.27
 * Script Purpose : Setting Character(Player)'s status
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_SuHyeon : Player
{
    [SerializeField] private DBManager_Player PlayerDB;
    [SerializeField] private GameObject Player;
    private CharacterController characterCont;
    public UIManager UI;
    private List<GameObject> monsterList = new List<GameObject>(); // Specify monster which gonna give the damage

    #region Properties

    public bool ActivatingPlayer
    {
        get { return activatingPlayer; }
        set
        {
            activatingPlayer = value;

            if (activatingPlayer)
            {
                if (CheckActivityCoroutine == null)
                    CheckActivityCoroutine = StartCoroutine(CheckActivity());
            }
            else
            {
                StopCoroutine(CheckActivityCoroutine);
                CheckActivityCoroutine = null;
            }
        }
    }

    public override float EXP
    {
        get { return Exp; }
        set
        {
            Exp = value;
            if (Exp >= PlayerDB.ExpList[level - 1])
            {
                Exp = Exp - PlayerDB.ExpList[level - 1];
                level++;
                UI.ShowSlotMachinePanel();
            }
        }
    }

    #endregion

    #region Coroutine

    //Coroutine AttackCoroutine; /* [Obsoleted] Use CharacterController -> characterCont.AttackCoroutine */
    Coroutine CheckActivityCoroutine;
    Coroutine EncroachCoroutine;

    #endregion

    private void Awake()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        PlayerDB.LoadingCharacterData(0); // Load character DB
        characterCont = GetComponent<CharacterController>();
        StartCoroutine(DataSet()); // Set character DB
    }

    private void Start()
    {
        Init();
        ActivatingPlayer = true;
    }

    private void Init()
    {
        level = 1;
        EXP = 0;
    }

    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            if (PlayerDB.isLoaded)
            {
                MaxHP = PlayerDB.hp;
                HP = PlayerDB.hp;
                AtkDamage = PlayerDB.damage;
                AtkSpeed = PlayerDB.attackSpeed;
                AtkRange = PlayerDB.attackRange;
                AtkRadius = PlayerDB.attackRadius;
                CriticalProb = PlayerDB.criticalProb;
                CriticalDamage = PlayerDB.criticalDamage;
                BloodSucking = PlayerDB.bloodSucking;
                MoveSpeed = PlayerDB.moveSpeed;
                AtkCount = PlayerDB.NumberOfTargets;
                SkillCoolTime = PlayerDB.skillCoolTime;

                DataLoading = false;
            }

            yield return null;
        }

        characterCont.speed = MoveSpeed; // Apply speed param to local player status
        yield return null;
    }

    private IEnumerator CheckActivity()
    {
        while (ActivatingPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (EncroachCoroutine == null)
                {
                    EncroachCoroutine = StartCoroutine(Encroach());
                }
            }

            yield return null;
        }
    }

    #region Attack Param

    public float radius; // Using for calculating range for skills
    private Vector3 playerDir;
    private float currentAngle; // Using for rotating Ray direction
    public float rotAngle;
    public float segments = 60; // How many rays will you shoot while sweeping one time

    #endregion

    /// <summary>
    /// Activate Coroutine When Player want to play basic attack
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Attack()
    {
        currentAngle = 22.5f;
        radius = 3;
        rotAngle = 45 / segments; // Each angles that rotate every ray the player shoot
        playerDir = Player.transform.forward;
        for (float i = currentAngle; i >= -22.5f; i -= rotAngle)
        {
            Quaternion
                rayDir = Quaternion.AngleAxis(i,
                    Vector3.up); // Rotate Ray Dir to Calc attack range -22.5f ~ 22.5f (Total 45 degree)
            Vector3 endPos = rayDir * playerDir * radius;

            RaycastHit[] hits;
            var origin = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.4f,
                Player.transform.position.z);
#if UNITY_EDITOR
            Debug.DrawRay(origin, endPos, Color.yellow, 1f);    
#endif

            hits = Physics.RaycastAll(origin, endPos, radius);

            for (int j = 0; j < hits.Length; j++)
            {
                RaycastHit hit = hits[j];
                if (hit.transform.CompareTag("Monster") )
                {
                    if(monsterList.Count<5 && !monsterList.Contains(hit.transform.gameObject))
                    {
                        monsterList.Add(hit.transform.gameObject);    
                    }
                }
            }
        }

        foreach (var t in monsterList)
        {
            //Attack to each monster
            t.GetComponent<MonsterBase>().HpChanged(-DamageCalc());
        }

        monsterList.Clear();

        yield return new WaitForSeconds(AtkSpeed);
        characterCont.AttackCoroutine = null;
        characterCont.isAttacking = false;
        yield break;
    }

    private float DamageCalc()
    {
        float Damage = AtkDamage;
        float per = Random.Range(0.0f, 100.0f);
        if (per >= 0f && per <= CriticalProb)
        {
            Damage = Damage * CriticalDamage;
            Debug.Log("Critical!!");
        }

        return Damage;
    }

    /// <summary>
    /// Player Kim's original skill
    /// </summary>
    /// <returns></returns>
    public IEnumerator Encroach()
    {
        float timer = 0;
        AtkDamage += 10;
        BloodSucking += 20;
        var speedUp = AtkSpeed * 0.3f;
        AtkSpeed += speedUp;

        while (timer <= 20)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        AtkDamage -= 10;
        BloodSucking -= 20;
        AtkSpeed -= speedUp;

        while (timer <= 60)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        EncroachCoroutine = null;

        yield return null;
    }
}
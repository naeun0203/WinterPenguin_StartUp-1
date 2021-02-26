/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.19
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
    private List<GameObject> monsterList = new List<GameObject>(); // Specify monster which gonna give the damage

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

    #region Coroutine
    //Coroutine AttackCoroutine; /* [Obsoleted] Use CharacterController -> characterCont.AttackCoroutine */
    Coroutine CheckActivityCoroutine;
    Coroutine EncroachCoroutine;
    #endregion

    private void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        PlayerDB.LoadingCharacterData(0); // Load character DB
        characterCont = GetComponent<CharacterController>();
        StartCoroutine(DataSet()); // Set character DB
        ActivatingPlayer = true;
    }

    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            if (PlayerDB.isLoaded)
            {
                MaxHP = PlayerDB.hp; // Init Max HP
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
            Quaternion rayDir = Quaternion.AngleAxis(i, Vector3.up); // Rotate Ray Dir to Calc attack range -22.5f ~ 22.5f (Total 45 degree)
            Vector3 endPos = rayDir * playerDir * radius;
            Debug.DrawRay(Player.transform.position, endPos, Color.yellow, 1f);
            RaycastHit hit;
            if (Physics.Raycast(Player.transform.position, endPos, out hit, radius))
            {
                if (hit.transform.CompareTag("Monster")) // If ray hit the monster
                {
                    if (monsterList.Count < AtkCount && !monsterList.Contains(hit.transform.gameObject))
                    {
                        monsterList.Add(hit.transform.gameObject); // Add monster GameObject in the list
                    }
                    else
                    {
                        break; // If monsterList contains 5 monsters, Just stop shooting ray
                    }
                }
            }   
            
        }

        for (int i = 0; i < monsterList.Count; i++)
        {
            //Attack to each monster
            monsterList[i].GetComponent<MonsterMelee>().HP -= AtkDamage;
        }
        monsterList.Clear(); // Reset Monster List

        yield return new WaitForSeconds(AtkSpeed);
        characterCont.AttackCoroutine = null;
        characterCont.isAttacking = false;
        yield return null;
    }

    private float DamageCalc()
    {
        float damage = 0;
        float critical;



        return damage;
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

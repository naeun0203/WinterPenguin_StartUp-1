/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.18
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
        set { 
            activatingPlayer = value;

            if (activatingPlayer)
            {
                if(CheckActivityCoroutine == null)
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
    Coroutine AttackCoroutine;
    Coroutine CheckActivityCoroutine;
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if(AttackCoroutine == null)
                {
                    AttackCoroutine = StartCoroutine(Attack());

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
    private IEnumerator Attack()
    {
        Debug.Log("AttackCoroutine Start");
        playerDir = Player.transform.forward.normalized;
        currentAngle = 22.5f;
        radius = 3;
        rotAngle = 45 / segments; // Each angles that rotate every ray the player shoot
        float x; // Ray destination x for each seconds;
        float y; // Ray destination y for each seconds;

        for (float i = currentAngle; i > -22.5f; i -= rotAngle)
        {
            Quaternion startDir = Quaternion.AngleAxis(currentAngle, Vector3.up); // Rotate Ray Dir to Calc attack range -22.5f ~ 22.5f (Total 45 degree)
            Vector3 startPos = startDir * playerDir * radius;
            Vector3 direction = startPos - Player.transform.position;
            RaycastHit hit;
            if(Physics.Raycast(Player.transform.position, direction, out hit))
            {
                if (hit.transform.CompareTag("Zombie")) // If ray hit the monster
                {
                    if(monsterList.Count <= AtkCount)
                    {
                        monsterList.Add(hit.transform.gameObject); // Add monster GameObject in the list
                    }
                    else
                    {
                        break; // If monsterList contains 5 monsters, Just stop shooting ray
                    }
                }
            }
            Debug.DrawRay(Player.transform.position, direction, Color.yellow, 1f);
        }

        for(int i = 0; i < monsterList.Count; i++)
        {
            //monsterList[i].GetComponent<MonsterManager>().hp
        }
        yield return new WaitForSeconds(AtkSpeed);
        AttackCoroutine = null;
        yield return null;
    }
}
